using Microsoft.Web.WebView2.Core;
using System.IO;
using System.Windows;

namespace PugBoat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //WebView2 Env for whole app
        //Init is async so just init at startup
        public static CoreWebView2Environment? WebView2Environment;

        public App()
        {
            InitWebView();
        }

        public async static Task InitWebView()
        {
            if (WebView2Environment != null)
            {
                //Avoid double init
                return;
            }
            else
            {
                var WebviewArgu = "--disable-features=msSmartScreenProtection,ElasticOverscroll --enable-features=msWebView2EnableDraggableRegions --disable-web-security --no-sandbox";
                CoreWebView2EnvironmentOptions options = new()
                {
                    AdditionalBrowserArguments = WebviewArgu
                };
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\WebviewCache\");
                WebView2Environment = await CoreWebView2Environment.CreateAsync(null, Environment.CurrentDirectory + "\\WebviewCache\\", options);
                WebView2Environment.BrowserProcessExited += (o, e) =>
                {
                    Console.WriteLine("WebView Environment Dead");
                    WebView2Environment = null;
                    //I just want webview always available
                    //There should be no chance all webviews destroyed so it should never reach here
                    InitWebView();
                };
            }
        }
    }

}
