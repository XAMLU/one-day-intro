using GitHubBrowser.Messages;
using GitHubBrowser.Services;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;

namespace GitHubBrowser.Views
{
    public sealed partial class ShellPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MessageService _messageService;

        public ShellPage()
        {
            InitializeComponent();
            _messageService = new MessageService();
            _messageService.SubscribeToGithubStatusChanged(this, HandleAuthChanged);
        }

        private void HandleAuthChanged(GithubStatusChangedMessage message)
        {
            EnableNavigation = message.AllowNavigation;
        }

        private bool _enableNavigation = false;
        public bool EnableNavigation
        {
            get => _enableNavigation;
            set
            {
                _enableNavigation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableNavigation)));
            }
        }
        private void NewIssue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var nav = new NavigationService();
            nav.Navigate(typeof(Views.CreateIssue));
        }
    }
}
