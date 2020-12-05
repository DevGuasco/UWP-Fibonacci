using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP_Fibonacci.View
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Back_ButtonFromSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Italian_Checked(object sender, RoutedEventArgs e)
        {
            ApplicationLanguages.PrimaryLanguageOverride = "it-IT";
            Frame.Navigate(GetType());
        }

        private void English_Checked(object sender, RoutedEventArgs e)
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            Frame.Navigate(GetType());
        }
    }
}