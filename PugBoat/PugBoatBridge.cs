using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
