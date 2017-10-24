using OneDayWorkshop01.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using OneDayWorkshop01.Services;

namespace OneDayWorkshop01
{
    sealed partial class App : Application
    {
        private static ShellPage _shell;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Window.Current.Content = _shell = new ShellPage();
            NavigationService.Setup(_shell.MainNavigationView);
            Window.Current.Activate();
        }
    }
}