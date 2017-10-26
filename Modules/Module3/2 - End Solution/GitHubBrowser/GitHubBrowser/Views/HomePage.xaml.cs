using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GitHubBrowser.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page, INotifyPropertyChanged
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += this.HomePage_Loaded;
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.LoginAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _loggedIn;
        public bool LoggedIn
        {
            get => _loggedIn;
            set
            {
                _loggedIn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoggedIn)));
                ShowLoggedInUI = _loggedIn;
                ShowLoggedOutUI = !_loggedIn;
            }
        }

        private bool _showLoggedOutUI = true;
        public bool ShowLoggedOutUI
        {
            get => _showLoggedOutUI;
            set
            {
                _showLoggedOutUI = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowLoggedOutUI)));
            }
        }

        private bool _showLoggedInUI;
        public bool ShowLoggedInUI
        {
            get => _showLoggedInUI;
            set
            {
                _showLoggedInUI = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowLoggedInUI)));
            }
        }

        private bool _showWaitUI;
        public bool ShowWaitUI
        {
            get => _showWaitUI;
            set
            {
                _showWaitUI = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowWaitUI)));
            }
        }

        private XamlU.Demo.GitHubLibrary.Models.GitHubUser _user;
        public XamlU.Demo.GitHubLibrary.Models.GitHubUser User
        {
            get => _user;
            set
            {
                _user = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(User)));
            }
        }

        public async Task LoginAsync()
        {
            var gitHubService = new GitHubBrowser.Services.GitHubService();
            ShowWaitUI = true;
            ShowLoggedOutUI = false;
            ShowLoggedInUI = false;

            LoggedIn = await gitHubService.AuthenticateAsync(true);
            if (!LoggedIn)
            {
                LoggedIn = await gitHubService.AuthenticateAsync();
            }
            if (LoggedIn)
            {
                User = await gitHubService.GetUserAsync();
            }

            if (LoggedIn)
            {
                ShowLoggedInUI = true;
                ShowLoggedOutUI = false;
            }
            else
            {
                ShowLoggedInUI = false;
                ShowLoggedOutUI = true;
            }

            ShowWaitUI = false;
        }
    }
}
