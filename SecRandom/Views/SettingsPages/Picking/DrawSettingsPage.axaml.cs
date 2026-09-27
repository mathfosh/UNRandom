using Avalonia.Controls;
using SecRandom.Core.Attributes;
using SecRandom.Core.Icons;

namespace SecRandom.Views.SettingsPages.Picking;

/// <summary>
///     统一抽取设置页：把默认抽取、点名抽取、闪抽三份设置合并在一页内分组展示。
/// </summary>
[PageInfo("settings.picking.draw", FluentIcons.DocumentBulletListCubeFilled)]
public partial class DrawSettingsPage : UserControl
{
    public DrawSettingsPage()
    {
        InitializeComponent();
    }
}
