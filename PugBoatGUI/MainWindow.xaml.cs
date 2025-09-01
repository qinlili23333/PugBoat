using Microsoft.Web.WebView2.Core;
using System.Windows;

namespace PugBoat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private async void Init()
        {
            await App.InitWebView();
            await WebView.EnsureCoreWebView2Async(App.WebView2Environment);
            WebView.CoreWebView2.AddHostObjectToScript("PugBoat", new PugBoatBridge(this));
            WebView.CoreWebView2.SetVirtualHostNameToFolderMapping("pugboat.local.qinlili.bid", "Res\\Hybrid", CoreWebView2HostResourceAccessKind.DenyCors);
            WebView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = false;
            WebView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
            WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
            WebView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            WebView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            WebView.CoreWebView2.Navigate("https://pugboat.local.qinlili.bid/Init.html");
        }
    }
}