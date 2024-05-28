#if WINDOWS
using WinUIEx;
#endif

namespace QuizUp.MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private void OnMaximizeClicked(object sender, EventArgs e)
    {
#if WINDOWS
        var window = this.Window.Handler.PlatformView as Microsoft.UI.Xaml.Window;
        window!.Maximize(); // Use WinUIEx Extension method to maximize window
#endif
    }

    private void OnFullScreenClicked(object sender, EventArgs e)
    {
#if WINDOWS
        // Get the window manager
        var window = this.Window.Handler.PlatformView as Microsoft.UI.Xaml.Window;
        var manager = WinUIEx.WindowManager.Get(window!);
        if (manager.PresenterKind == Microsoft.UI.Windowing.AppWindowPresenterKind.Overlapped)
            manager.PresenterKind = Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen;
        else
            manager.PresenterKind = Microsoft.UI.Windowing.AppWindowPresenterKind.Overlapped;
#endif
    }
}
