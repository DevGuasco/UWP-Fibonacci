using System;
using UWP_Fibonacci.View;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP_Fibonacci
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public void OnLaunchRequestedSize()
        {
            float DPI = Windows.Graphics.Display.DisplayInformation.GetForCurrentView().LogicalDpi;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            var desiredSize = new Windows.Foundation.Size(800 * 60.0f / DPI, 600 * 96.0f / DPI);
            ApplicationView.PreferredLaunchViewSize = desiredSize;
            Window.Current.Activate();
            bool result = ApplicationView.GetForCurrentView().TryResizeView(desiredSize);
        }

        public void CustomTitleBar()
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            #region Active TitleBar Color

            titleBar.ForegroundColor = Windows.UI.Colors.White;
            titleBar.BackgroundColor = Windows.UI.Colors.Black;
            titleBar.ButtonForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Black;
            titleBar.ButtonHoverForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(60, 60, 60, 60);
            titleBar.ButtonPressedForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(90, 90, 90, 90);

            #endregion Active TitleBar Color

            #region Inactive TitleBar Color

            titleBar.InactiveForegroundColor = Windows.UI.Colors.White;
            titleBar.InactiveBackgroundColor = Windows.UI.Colors.Black;
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Black;

            #endregion Inactive TitleBar Color
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            CustomTitleBar();
            OnLaunchRequestedSize();

            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}