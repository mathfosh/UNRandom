using CommunityToolkit.Mvvm.ComponentModel;
using SecRandom.Core.Enums.Configs;
using SecRandom.Core.Helpers;

namespace SecRandom.Core.Models.SubConfigs.Picking;

public partial class LotterySettingsConfig : DrawSettingsConfigBase
{
    [ObservableProperty] private DrawMode _drawMode = DrawMode.NoRepeat;
    [ObservableProperty] private ClearRecordMode _clearRecord = ClearRecordMode.Restarted;
    [ObservableProperty] private int _halfRepeat = 1;

    [ObservableProperty] private LotteryDrawType _drawType = LotteryDrawType.Count;
    [ObservableProperty] private string _algorithmId = "builtin.inventory";
    [ObservableProperty] private string _defaultPool = string.Empty;
    [ObservableProperty] private LotteryShowRandomMode _lotteryShowRandom = LotteryShowRandomMode.PrizeIdPrizeBreakGroupHyphenMember;
    [ObservableProperty] private string _customLotteryShowRandomFormat = LotteryProcessDisplayFormatter.DefaultTemplate;
    [ObservableProperty] private bool _lotteryImage = false;
    [ObservableProperty] private StudentImagePositionMode _lotteryImagePosition = StudentImagePositionMode.Left;

    // 抽奖仍保留自己的显示覆盖开关，点名与闪抽的覆盖开关已随配置合一移除。
    [ObservableProperty] private bool _overrideDisplaySettings = false;
    [ObservableProperty] private bool _overrideAnimationSettings = false;
    [ObservableProperty] private bool _overrideColorSettings = false;
    [ObservableProperty] private bool _overrideStudentImageSettings = false;
    [ObservableProperty] private bool _overrideReminderSettings = false;
    [ObservableProperty] private bool _overrideMusicSettings = false;
    [ObservableProperty] private bool _overrideVoiceAnnouncementSettings = false;
}
