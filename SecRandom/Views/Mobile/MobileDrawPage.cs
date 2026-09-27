using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
using SecRandom.Core.Abstraction;
using SecRandom.Core.Abstraction.Services;
using SecRandom.Services.ViewEngine;
using SecRandom.ViewModels.MainPages;
using RollCallResources = SecRandom.Langs.MainPages.RollCall.Resources;

namespace SecRandom.Views.Mobile;

/// <summary>
/// Mobile layout shell over the desktop roll-call draw session. Draw state, result projection, and animations remain shared.
/// </summary>
public sealed partial class MobileDrawPage : UserControl
{
    private readonly TabStrip _drawSurfaceTabs;

    public MobileDrawPage(RollCallPageViewModel rollCallViewModel)
    {
        RollCallViewModel = rollCallViewModel;
        DataContext = this;
        InitializeComponent();
        _drawSurfaceTabs = this.FindControl<TabStrip>("DrawSurfaceTabs")!;
        RollCallViewModel.PropertyChanged += DrawViewModelOnPropertyChanged;
        DetachedFromVisualTree += (_, _) =>
        {
            RollCallViewModel.PropertyChanged -= DrawViewModelOnPropertyChanged;
        };
    }

    public RollCallPageViewModel RollCallViewModel { get; }
    public bool CanChangeSurface => !RollCallViewModel.IsDrawing;

    private void DrawSurfaceTabs_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // Avalonia raises the initial selection event while InitializeComponent is still populating
        // the control tree, before the named fields have been assigned by the constructor.
        if (_drawSurfaceTabs is null || !ReferenceEquals(sender, _drawSurfaceTabs))
            return;

        // 抽奖面板已移除，抽取面板始终是索引 0。
        if (_drawSurfaceTabs.SelectedIndex != 0)
            _drawSurfaceTabs.SelectedIndex = 0;
    }

    private void DrawViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RollCallPageViewModel.IsDrawing))
            _drawSurfaceTabs.IsEnabled = CanChangeSurface;
    }

    private void ShowRollCallRemaining_OnClick(object? sender, RoutedEventArgs e)
    {
        RollCallViewModel.RefreshRemainingList();
        _ = IAppHost.GetService<RemainingListViewService>().ShowAsync(
            RollCallResources.C_RemainingListTitle, RollCallViewModel.RemainingItems, RollCallResources.M_NoRemainingStudents);
    }

    private void ClearRollCallTemporaryRecords_OnClick(object? sender, RoutedEventArgs e)
    {
        IAppHost.GetService<IDrawTemporaryRecordService>().ClearStudentList(RollCallViewModel.SelectedStudentListName);
        RollCallViewModel.RefreshAfterProfileChange();
    }

    private void OpenRollCallListSettings_OnClick(object? sender, RoutedEventArgs e) =>
        App.ShowSettingsWindow("settings.listManagement.rollCallList");

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
