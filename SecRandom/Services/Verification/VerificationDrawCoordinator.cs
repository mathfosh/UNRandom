using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SecRandom.Core.Enums;
using SecRandom.Core.Models.Verification;
using SecRandom.Core.Services.Draw;
using SecRandom.Core.Services.Verification;
using SecRandom.Shared.Models.Profile;

namespace SecRandom.Services.Verification;

/// <summary>
///     抽取执行协调器：用确定性随机内核完成点名、闪抽与抽奖的抽取计算，并冻结权重快照供提交使用。
/// </summary>
public sealed class VerificationDrawCoordinator(
    DrawEngine drawEngine,
    IVerificationKernel kernel)
{
    public bool IsEnabled => true;

    public Task<VerificationDrawOutcome<Student>> DrawStudentsAsync(
        int count,
        IReadOnlyCollection<Student> candidates,
        DrawSettingsType drawSettingsType,
        string courseName = "",
        CancellationToken cancellationToken = default)
    {
        var input = drawEngine.CreateStudentVerificationInput(count, candidates, drawSettingsType, courseName, true);
        return DrawAsync(input, candidates, cancellationToken);
    }

    public Task<VerificationDrawOutcome<Prize>> DrawPrizesAsync(
        int count,
        IReadOnlyDictionary<string, int> temporaryCounts,
        IReadOnlyCollection<Prize> prizes,
        CancellationToken cancellationToken = default)
    {
        var input = drawEngine.CreatePrizeVerificationInput(count, temporaryCounts, true);
        return DrawAsync(input, prizes, cancellationToken);
    }

    private Task<VerificationDrawOutcome<TCandidate>> DrawAsync<TCandidate>(
        VerificationDrawInput input,
        IReadOnlyCollection<TCandidate> records,
        CancellationToken cancellationToken)
        where TCandidate : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        var recordLookup = records.ToDictionary(GetRecordId);
        var seed = VerificationSeedDerivation.CreateCsprngSeed();
        var result = kernel.Draw(input, seed);
        var winners = result.Winners
            .Select(winner => recordLookup.TryGetValue(winner.RecordId, out var record)
                ? record
                : throw new InvalidDataException("抽取内核返回了冻结候选池之外的记录。"))
            .ToList();
        return Task.FromResult(new VerificationDrawOutcome<TCandidate>(winners, Guid.NewGuid(), FreezeWeights(input)));
    }

    private static IReadOnlyDictionary<Guid, double> FreezeWeights(VerificationDrawInput input)
    {
        // 提交侧的权重快照必须取自冻结输入；同一记录多次出现（奖品库存）取首个权重。
        return input.Candidates
            .GroupBy(candidate => candidate.RecordId)
            .ToDictionary(group => group.Key, group => group.First().WeightMicros / 1_000_000d);
    }

    private static Guid GetRecordId<TCandidate>(TCandidate candidate) where TCandidate : class
    {
        return candidate switch
        {
            Student student when student.RecordId != Guid.Empty => student.RecordId,
            Prize prize when prize.RecordId != Guid.Empty => prize.RecordId,
            Student student => EnsureRecordId(student),
            Prize prize => EnsureRecordId(prize),
            _ => throw new ArgumentException("仅支持学生与奖品记录。", nameof(candidate))
        };
    }

    private static Guid EnsureRecordId(Student student)
    {
        ProfileRecordIdentity.EnsureRecordId(student);
        return student.RecordId;
    }

    private static Guid EnsureRecordId(Prize prize)
    {
        ProfileRecordIdentity.EnsureRecordId(prize);
        return prize.RecordId;
    }
}

public sealed record VerificationDrawOutcome<TCandidate>(
    IReadOnlyList<TCandidate> Winners,
    Guid ProofId,
    IReadOnlyDictionary<Guid, double> FrozenWeights);
