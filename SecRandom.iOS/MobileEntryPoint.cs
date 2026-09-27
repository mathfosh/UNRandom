#if IOS
using Avalonia;
using Avalonia.iOS;
using CoreGraphics;
using Foundation;
using SecRandom.Core.Abstraction;
using SecRandom.Platforms;
using SecRandom.Platforms.Abstractions;
using SecRandom.Mobile;
using System.Runtime.Versioning;
using UIKit;

namespace SecRandom.Mobile.iOS;

[SupportedOSPlatform("ios13.0")]
public static class MobileEntryPoint
{
    public static void Main(string[] args)
    {
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}

[SupportedOSPlatform("ios13.0")]
[Register("AppDelegate")]
public sealed class AppDelegate : AvaloniaAppDelegate<global::SecRandom.App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        PlatformStartupContext.Set(new MobilePlatformServiceRoot(PlatformKind.Ios)
        {
            MediaPlayer = new IosMobileMediaPlayer(),
            KeyboardOcclusionSource = new IosKeyboardOcclusionSource(),
            UsesDesktopMainView = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad
        });
        return base.CustomizeAppBuilder(builder);
    }
}

[SupportedOSPlatform("ios13.0")]
internal sealed class IosKeyboardOcclusionSource : IMobileKeyboardOcclusionSource
{
    private readonly NSObject[] _keyboardObservers;

    public IosKeyboardOcclusionSource()
    {
        _keyboardObservers =
        [
            NSNotificationCenter.DefaultCenter.AddObserver(
                UIKeyboard.WillShowNotification,
                notification => Publish(notification.UserInfo)),
            NSNotificationCenter.DefaultCenter.AddObserver(
                UIKeyboard.WillChangeFrameNotification,
                notification => Publish(notification.UserInfo)),
            NSNotificationCenter.DefaultCenter.AddObserver(
                UIKeyboard.WillHideNotification,
                notification => Publish(notification.UserInfo))
        ];
    }

    public event EventHandler<MobileKeyboardOcclusionChangedEventArgs>? Changed;

    private void Publish(NSDictionary? userInfo)
    {
        var window = UIApplication.SharedApplication.ConnectedScenes
            .OfType<UIWindowScene>()
            .SelectMany(scene => scene.Windows)
            .Where(candidate => !candidate.Hidden && candidate.Bounds.Height > 0)
            .OrderByDescending(candidate => candidate.IsKeyWindow)
            .FirstOrDefault();
        if (window is null || userInfo?[UIKeyboard.FrameEndUserInfoKey] is not NSValue frameValue)
            return;

        // Convert the screen-space keyboard frame into the app window's local coordinates.
        var keyboardFrame = window.ConvertRectFromView(frameValue.CGRectValue, null);
        var intersectionTop = Math.Max(window.Bounds.Top, keyboardFrame.Top);
        var intersectionBottom = Math.Min(window.Bounds.Bottom, keyboardFrame.Bottom);
        var occludedHeight = Math.Max(0, intersectionBottom - intersectionTop);
        var duration = userInfo[UIKeyboard.AnimationDurationUserInfoKey] is NSNumber durationValue
            ? TimeSpan.FromSeconds(durationValue.DoubleValue)
            : TimeSpan.Zero;
        Changed?.Invoke(this, new MobileKeyboardOcclusionChangedEventArgs(occludedHeight, duration));
    }
}
#endif
