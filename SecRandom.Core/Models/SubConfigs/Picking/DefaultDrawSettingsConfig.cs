using CommunityToolkit.Mvvm.ComponentModel;
using SecRandom.Core.Enums.Configs;

namespace SecRandom.Core.Models.SubConfigs.Picking;

/// <summary>
///     默认抽取设置：点名与闪抽共用这一份配置，不再各自维护独立设置与覆盖项。
/// </summary>
public partial class DefaultDrawSettingsConfig : DrawSettingsConfigBase
{
    [ObservableProperty] private DrawMode _drawMode = DrawMode.NoRepeat;
    [ObservableProperty] private int _halfRepeat = 1;
    [ObservableProperty] private ClearRecordMode _clearRecord = ClearRecordMode.Restarted;
    [ObservableProperty] private DrawType _drawType = DrawType.Fair;
    [ObservableProperty] private string _algorithmId = "builtin.fair";
    [ObservableProperty] private string _defaultClass = string.Empty;
    [ObservableProperty] private int _disableAfterClick = 1;
}
