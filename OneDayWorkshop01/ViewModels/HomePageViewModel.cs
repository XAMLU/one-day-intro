using System.Threading.Tasks;
using GitHubBrowser.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using XamlU.Demo.GitHubLibrary.Models;

namespace GitHubBrowser.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        private GitHubService _gitHubService;
        private SettingsService _settingService;
        private MessageService _messageService;

        public HomePageViewModel()
        {
            ShowWaitUI = true;
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled
                || Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                return;
            }
            _gitHubService = new GitHubService();
            _settingService = new SettingsService();
            _messageService = new MessageService();
        }

        private string _searchQuery = "xamlu";
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                Set(ref _searchQuery, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get => _searchCommand ?? (_searchCommand = new RelayCommand(SearchExecute, SearchCanExecute));
        }
        private async void SearchExecute()
        {
            Results.Clear();
            var result = await _gitHubService.SearchRepositoriesAsync(SearchQuery);
            foreach (var item in result)
            {
                Results.Add(item);
            }
        }
        private bool SearchCanExecute()
        {
            return !string.IsNullOrEmpty(SearchQuery);
        }

        public ObservableCollection<SearchRepositoriesItem> Results { get; }
            = new ObservableCollection<SearchRepositoriesItem>();

        private SearchRepositoriesItem _selectedRespository;
        public SearchRepositoriesItem SelectedRepository
        {
            get => _selectedRespository;
            set
            {
                Set(ref _selectedRespository, value);
                MakeDefaultCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _makeDefaultCommand;
        public RelayCommand MakeDefaultCommand
        {
            get => _makeDefaultCommand ?? (_makeDefaultCommand = new RelayCommand(MakeDefaultExecute, MakeDefaultCanExecute));
        }
        private void MakeDefaultExecute()
        {
            DefaultRepository = _settingService.DefaultRepository = SelectedRepository.full_name;
            _messageService.SendGithubStatusChanged();
        }
        private bool MakeDefaultCanExecute()
        {
            return !string.IsNullOrWhiteSpace(SelectedRepository?.full_name);
        }

        private string _defaultRepository;
        public string DefaultRepository
        {
            get => _settingService.DefaultRepository ?? "None";
            set => Set(ref _defaultRepository, value);
        }

        private bool _loggedIn;
        public bool LoggedIn
        {
            get => _loggedIn;
            set
            {
                Set(ref _loggedIn, value);
                ShowLoggedInUI = _loggedIn;
                ShowLoggedOutUI = !_loggedIn;
            }
        }

        private bool _showLoggedOutUI;
        public bool ShowLoggedOutUI
        {
            get => _showLoggedOutUI;
            set => Set(ref _showLoggedOutUI, value);
        }

        private bool _showLoggedInUI;
        public bool ShowLoggedInUI
        {
            get => _showLoggedInUI;
            set => Set(ref _showLoggedInUI, value);
        }

        private bool _showWaitUI;
        public bool ShowWaitUI
        {
            get => _showWaitUI;
            set => Set(ref _showWaitUI, value);
        }

        private GitHubUser _user;
        public GitHubUser User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        public async Task LoginAsync()
        {
            LoggedIn = await _gitHubService.AuthenticateAsync(true);
            if (!LoggedIn)
            {
                LoggedIn = await _gitHubService.AuthenticateAsync();
            }
            if (LoggedIn)
            {
                User = await _gitHubService.GetUserAsync();
            }
            ShowWaitUI = false;
        }
    }
}
