using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SecRandom.Core.Models.SubConfigs.General;

public partial class GeneralSettingsConfig : ObservableObject
{
    [ObservableProperty] private BasicSettingsConfig _basic = new();
    [ObservableProperty] private BackupConfig _backup = new();
    [ObservableProperty] private CrashRecoverySettingsConfig _crashRecovery = new();

    public void ApplyLegacyBasic(BasicSettingsConfig? legacyBasic)
    {
        if (legacyBasic is null)
            return;

        Basic = legacyBasic;
    }

    public void ApplyLegacyBackup(BackupConfig? legacyBackup)
    {
        if (legacyBackup is null)
            return;

        Backup = legacyBackup;
    }
}
