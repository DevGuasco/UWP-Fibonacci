using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Resources;

namespace UWP_Fibonacci.View
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            StatusBar.Visibility = Visibility.Collapsed;
            StatusBar.IsEnabled = false;
        }

        public long Fibonacci(int n)
        {
            if (n == 0 || n == 1)
                return n;
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        public void ShowToast()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string msg = resourceLoader.GetString("completedTaskMessage");
            string subMsg = resourceLoader.GetString("completedTaskSubmessage");
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(subMsg));
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public void UpdateUserInterface()
        {
            dataInput.Text = "";
            resultOutput.Text = "";
            elapsedTime.Text = "";
            StatusBar.Visibility = Visibility.Collapsed;
            StatusBar.IsEnabled = false;
        }

        public void ShowProgressBar()
        {
            StatusBar.IsEnabled = true;
            StatusBar.Visibility = Visibility.Visible;
        }

        public void RemoveOldText()
        {
            resultOutput.Text = "";
            elapsedTime.Text = "";
        }

        public void HideProgressBar()
        {
            StatusBar.IsEnabled = false;
            StatusBar.Visibility = Visibility.Collapsed;
        }

        #region MainCalculateButton

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dataInput.Text) || string.IsNullOrWhiteSpace(dataInput.Text))
            {
                await this.blankInputDialog.ShowAsync();
                UpdateUserInterface();
            }
            else
            {
                try
                {
                    ShowProgressBar();
                    RemoveOldText();
                    var resourceLoader = ResourceLoader.GetForCurrentView();
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    int n = Convert.ToInt32(dataInput.Text);
                    resultOutput.Text = resourceLoader.GetString("resultOutput") + $"F ( {n} )...";
                    stopwatch.Start();
                    Task<long> fibonacciTask = Task.Run(() => Fibonacci(n));
                    await fibonacciTask;
                    stopwatch.Stop();
                    resultOutput.Text = $"F ( {n} )  =>  " + fibonacciTask.Result.ToString("N0", new NumberFormatInfo()
                    {
                        NumberGroupSizes = new[] { 3 },
                        NumberGroupSeparator = "."
                    });
                    elapsedTime.Text = resourceLoader.GetString("elapsedTime") + Math.Round(stopwatch.Elapsed.TotalMilliseconds / 1000, 4) + " sec";
                    HideProgressBar();
                    ShowToast();
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception}");
                }
            }
        }

        #endregion MainCalculateButton

        private void DataInput_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void BlankInputDialog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            blankInputDialog.Hide();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void TeachingTip_Click(object sender, RoutedEventArgs e)
        {
            MainTeachingTip.IsOpen = true;
        }

        private void UpdateInterface_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(GetType());
        }
    }
}