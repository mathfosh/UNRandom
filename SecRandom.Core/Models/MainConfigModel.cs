using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using SecRandom.Core.Enums;
using SecRandom.Core.Helpers;
using SecRandom.Core.Models.SubConfigs;
using SecRandom.Shared;
using SecRandom.Shared.Abstraction;
using AppearanceSettingsConfig = SecRandom.Core.Models.SubConfigs.Personalized.AppearanceSettingsConfig;
using BackupConfig = SecRandom.Core.Models.SubConfigs.General.BackupConfig;
using BasicSettingsConfig = SecRandom.Core.Models.SubConfigs.General.BasicSettingsConfig;
using GeneralSettingsConfig = SecRandom.Core.Models.SubConfigs.General.GeneralSettingsConfig;
using DefaultDrawSettingsConfig = SecRandom.Core.Models.SubConfigs.Picking.DefaultDrawSettingsConfig;
using DrawSettingsConfigBase = SecRandom.Core.Models.SubConfigs.Picking.DrawSettingsConfigBase;
using FairDrawSettingsConfig = SecRandom.Core.Models.SubConfigs.Picking.FairDrawSettingsConfig;
using LotterySettingsConfig = SecRandom.Core.Models.SubConfigs.Picking.LotterySettingsConfig;

namespace SecRandom.Core.Models;

public partial class MainConfigModel : ConfigBase, IJsonOnDeserialized
{
    [ObservableProperty] private FloatPositionConfig _floatPosition = new();

    // 通用
    [ObservableProperty] private GeneralSettingsConfig _general = new();

    // 个性化
    [ObservableProperty] private AppearanceSettingsConfig _appearance = new();

    // 抽取设置
    [ObservableProperty] private FairDrawSettingsConfig _fairDrawSettings = new();
    [ObservableProperty] private DefaultDrawSettingsConfig _defaultDrawSettings = new();
    [ObservableProperty] private LotterySettingsConfig _lotterySettings = new();

    [ObservableProperty] private FloatingWindowSettingsConfig _floatingWindowSettings = new();
    private NotificationSettingsConfig _notificationSettings = new();

    [AllowNull]
    public NotificationSettingsConfig NotificationSettings
    {
        get => _notificationSettings;
        set => SetProperty(ref _notificationSettings, value ?? new NotificationSettingsConfig());
    }
    [ObservableProperty] private SecuritySettingsConfig _securitySettings = new();
    [ObservableProperty] private LinkageSettingsConfig _linkageSettings = new();
    [ObservableProperty] private VoiceSettingsConfig _voiceSettings = new();
    [ObservableProperty] private HistoryManagementSettingsConfig _historyManagementSettings = new();
    [ObservableProperty] private MoreSettingsConfig _moreSettings = new();
    [ObservableProperty] private List<int> _recentTimerPresetSeconds = [];

    [JsonPropertyName("moreSettings")]
    public MoreSettingsConfig LegacyMoreSettingsOnLoad
    {
        set => MoreSettings = value;
    }

    [JsonIgnore] public override string ConfigFilePath => Utils.GetFilePath("config", "settings.json");

    [JsonIgnore]
    public BasicSettingsConfig Basic
    {
        get => General.Basic;
        set => General.Basic = value;
    }

    [JsonIgnore]
    public BackupConfig Backup
    {
        get => General.Backup;
        set => General.Backup = value;
    }

    [JsonPropertyName("basic")]
    public BasicSettingsConfig LegacyBasicOnLoad
    {
        set => General.ApplyLegacyBasic(value);
    }

    [JsonPropertyName("backup")]
    public BackupConfig LegacyBackupOnLoad
    {
        set => General.ApplyLegacyBackup(value);
    }

    private DefaultDrawSettingsConfig? _legacyRollCallSettings;
    private DefaultDrawSettingsConfig? _legacyQuickDrawSettings;

    /// <summary>
    ///     旧版本的点名抽取设置，仅用于读取老配置并并入默认抽取设置。
    /// </summary>
    [JsonPropertyName("roll_call_settings")]
    public DefaultDrawSettingsConfig? LegacyRollCallSettingsOnLoad
    {
        set => _legacyRollCallSettings = value;
    }

    /// <summary>
    ///     旧版本的闪抽抽取设置，仅用于读取老配置并并入默认抽取设置。
    /// </summary>
    [JsonPropertyName("quick_draw_settings")]
    public DefaultDrawSettingsConfig? LegacyQuickDrawSettingsOnLoad
    {
        set => _legacyQuickDrawSettings = value;
    }

    public string GetLotteryProcessDisplayTemplate()
    {
        return LotterySettings.OverrideDisplaySettings
            ? LotteryProcessDisplayFormatter.ResolveTemplate(
                LotterySettings.LotteryShowRandom,
                LotterySettings.CustomLotteryShowRandomFormat)
            : LotteryProcessDisplayFormatter.DefaultTemplate;
    }

    public NotificationChannelSettings GetNotificationChannelSettings(NotificationSettingsType notificationSettingsType)
    {
        return notificationSettingsType switch
        {
            NotificationSettingsType.RollCall => NotificationSettings.RollCall,
            NotificationSettingsType.QuickDraw => NotificationSettings.QuickDraw,
            NotificationSettingsType.Lottery => NotificationSettings.Lottery,
            _ => throw new ArgumentOutOfRangeException(
                nameof(notificationSettingsType), notificationSettingsType, null)
        };
    }

    public NotificationChannelSettings GetOverrideNotificationSettings(
        NotificationSettingsType notificationSettingsType,
        OverridableNotificationSettingsType settingsType)
    {
        var settings = (OverridableNotificationChannelSettings)GetNotificationChannelSettings(notificationSettingsType);
        return settingsType switch
        {
            OverridableNotificationSettingsType.Basic => settings,
            OverridableNotificationSettingsType.NotificationWindow => settings.OverrideNotificationWindowSettings
                ? settings
                : NotificationSettings.Default,
            OverridableNotificationSettingsType.Service => settings.OverrideServiceSettings
                ? settings
                : NotificationSettings.Default,
            _ => throw new ArgumentOutOfRangeException(nameof(settingsType), settingsType, null)
        };
    }

    void IJsonOnDeserialized.OnDeserialized()
    {
        ApplyLegacyAnimationMusicLoop();
        ApplyLegacyDrawSettings();
    }

    private void ApplyLegacyAnimationMusicLoop()
    {
        var legacyMusicLoop = MoreSettings.ConsumeLegacyBackgroundMusicLoop();
        if (legacyMusicLoop is not { } animationMusicLoop)
            return;

        ApplyLegacyAnimationMusicLoop(DefaultDrawSettings, animationMusicLoop);
        ApplyLegacyAnimationMusicLoop(LotterySettings, animationMusicLoop);
    }

    private static void ApplyLegacyAnimationMusicLoop(DrawSettingsConfigBase settings, bool value)
    {
        if (!settings.HasAnimationMusicLoop)
            settings.AnimationMusicLoop = value;
    }

    /// <summary>
    ///     点名与闪抽过去各存一份抽取设置，现在统一读默认抽取设置。
    ///     并入时先取闪抽独有的「点击后禁用」，再以旧点名设置覆盖其余抽取参数。
    ///     下一次保存配置会写掉这两个旧节点，迁移只生效一次。
    /// </summary>
    private void ApplyLegacyDrawSettings()
    {
        if (_legacyRollCallSettings is null && _legacyQuickDrawSettings is null)
            return;

        if (_legacyQuickDrawSettings is { } quickDraw)
            DefaultDrawSettings.DisableAfterClick = quickDraw.DisableAfterClick;

        if (_legacyRollCallSettings is not { } rollCall)
            return;

        DefaultDrawSettings.DrawMode = rollCall.DrawMode;
        DefaultDrawSettings.HalfRepeat = rollCall.HalfRepeat;
        DefaultDrawSettings.ClearRecord = rollCall.ClearRecord;
        DefaultDrawSettings.DrawType = rollCall.DrawType;
        DefaultDrawSettings.AlgorithmId = rollCall.AlgorithmId;
        DefaultDrawSettings.DefaultClass = rollCall.DefaultClass;
    }
}
