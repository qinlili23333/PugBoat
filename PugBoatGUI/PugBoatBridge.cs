namespace PugBoat
{
    public class PugBoatBridge
    {
        MainWindow CurrentWindow;
        public PugBoatBridge(MainWindow Window)
        {
            CurrentWindow = Window;
        }

        // Open Dev Tools
        public void OpenDevTools()
        {
            CurrentWindow.WebView.CoreWebView2.OpenDevToolsWindow();
        }

        // Show Window
        public void ShowWindow()
        {
            CurrentWindow.Visibility = System.Windows.Visibility.Visible;
        }

        // Hide Window
        public void HideWindow()
        {
            CurrentWindow.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
