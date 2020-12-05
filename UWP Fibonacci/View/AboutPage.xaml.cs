using System;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP_Fibonacci.View
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var package = Package.Current;
            var version = package.Id.Version;
            AppNameTextBlock.Text = package.DisplayName;
            AppVersionTextBlock.Text = string.Format("Version {0}.{1}.{2}", version.Major, version.Minor, version.Build);
            DevNameTextBlock.Text = package.PublisherDisplayName;
        }

        private async void FeedbackClick(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("mailto:simone.guasconi03@gmail.com?subject=" + AppNameTextBlock.Text);
            await Launcher.LaunchUriAsync(uri);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void GitHub_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://github.com/DevGuasco/UWP-Fibonacci");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
