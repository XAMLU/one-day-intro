using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OneDayWorkshop01.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OneDayWorkshop01.Views
{
    public sealed partial class HomePage : Page
    {
        private GitHubService _gitHubService;

        public HomePage()
        {
            this.InitializeComponent();
            _gitHubService = new GitHubService();
            Loaded += HomePage_Loaded;
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateUI();
        }

        private async Task UpdateUI()
        {
            var loggedin = await _gitHubService.AuthenticateAsync(true);
            if (!loggedin)
            {
                loggedin = await _gitHubService.AuthenticateAsync();
            }
            if (loggedin)
            {
                var user = await _gitHubService.GetUserAsync();
                UserNameTextBlock.Text = string.Format(UserNameTextBlock.Text, user.name);
                LoggedInUI.Visibility = Visibility.Visible;
                LoggedOutUI.Visibility = Visibility.Collapsed;
                WaitUI.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoggedInUI.Visibility = Visibility.Collapsed;
                LoggedOutUI.Visibility = Visibility.Visible;
                WaitUI.Visibility = Visibility.Collapsed;
            }
        }

        private async void Login_Clicked(object sender, RoutedEventArgs e)
        {
            await _gitHubService.AuthenticateAsync();
            await UpdateUI();
        }
    }
}
