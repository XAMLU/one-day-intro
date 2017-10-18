using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDayWorkshop01.Services;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using XamlU.Demo.GitHubLibrary.Models;

namespace OneDayWorkshop01.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        private GitHubService _githubService;
        private SettingsService _settingService;

        public HomePageViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled
                || !Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                _githubService = new GitHubService();
                _settingService = new SettingsService();
            }
        }

        private string _searchQuery;
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
            var result = await _githubService.SearchRepositoriesAsync(SearchQuery);
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
    }
}
