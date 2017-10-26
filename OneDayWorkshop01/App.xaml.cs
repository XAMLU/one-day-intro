using GitHubBrowser.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace GitHubBrowser
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var shell = new ShellPage();
            Window.Current.Content = shell;
            Window.Current.Activate();
        }
    }
}