using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SecRandom.Mobile;

namespace SecRandom.Views.Mobile;

public sealed partial class MobileHistoryPage : UserControl
{
    public MobileHistoryPage(IMobileCapabilities capabilities)
    {
        InitializeComponent();
    }

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
