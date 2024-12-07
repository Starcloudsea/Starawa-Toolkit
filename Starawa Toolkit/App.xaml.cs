using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Starawa_Toolkit.Views.Windows;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Starawa_Toolkit {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private IntPtr _windowHandle;

        public static AppWindow AppWindow { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) {
           //Create new window
            m_window = new MainWindow();

            //Set window
            if (SetWindow(m_window, 800, 500)) { } //TODO
            
            //Show window
            m_window.Activate();
        }

        /// <summary>
        /// After passing in the parameters, the Window parameters will be set with the width, height parameters.
        /// This will change the title bar and window size.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>If the AppWindow title bar is available on the current system, it returns True, otherwise it returns False.</returns>
        public bool SetWindow(Window window, int width, int height) {
            //Get window handle
            _windowHandle = WindowNative.GetWindowHandle(m_window);
            var windowId = Win32Interop.GetWindowIdFromWindow(_windowHandle);
            AppWindow = AppWindow.GetFromWindowId(windowId);
            var incps = InputNonClientPointerSource.GetForWindowId(windowId);

            //Set window size
            var ScreenHeight = DisplayArea.Primary.WorkArea.Height;
            var ScreenWidth = DisplayArea.Primary.WorkArea.Width;
            AppWindow.MoveAndResize(new RectInt32(ScreenWidth - ScreenWidth / 2 - width / 2, ( ScreenHeight - ScreenHeight / 2 - height / 2 ), width, height));

            //Set title bar
            AppWindow.Title = "Starawa Toolkit";
            if (AppWindowTitleBar.IsCustomizationSupported()) {
                AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                incps.SetRegionRects(NonClientRegionKind.Caption, [new RectInt32(40, 0, width - 40, 40)]);
                return true;
            }
            else {
                return false;
            }
        }

        private Window? m_window;
    };
}
