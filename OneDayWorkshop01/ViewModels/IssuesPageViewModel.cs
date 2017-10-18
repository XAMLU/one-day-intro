using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDayWorkshop01.Services;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using XamlU.Demo.GitHubLibrary.Models;

namespace OneDayWorkshop01.ViewModels
{
    class IssuesPageViewModel
    {
        private SettingsService _settingsService;
        private GitHubService _githubService;

        public IssuesPageViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled
               || !Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                _settingsService = new SettingsService();
                _githubService = new GitHubService();
                RefreshIssuesCommand.Execute(null);
            }
        }

        private RelayCommand _refreshIssuesCommand;
        public RelayCommand RefreshIssuesCommand
        {
            get => _refreshIssuesCommand ?? (_refreshIssuesCommand = new RelayCommand(RefreshIssuesExecute, RefreshIssuesCanExecute));
        }
        private async void RefreshIssuesExecute()
        {
            Issues.Clear();
            OpenIssues.Clear();
            ClosedIssues.Clear();

            var repo = _settingsService.DefaultRepository;
            var items = await _githubService.GetIssuesAsync(repo);

            foreach (var item in items.Issues)
            {
                Issues.Add(item);
            }
            foreach (var item in items.Issues.Where(x => x.state.Equals("open")))
            {
                OpenIssues.Add(item);
            }
            foreach (var item in items.Issues.Where(x => !x.state.Equals("open")))
            {
                ClosedIssues.Add(item);
            }
        }
        private bool RefreshIssuesCanExecute()
        {
            return true;
        }

        public ObservableCollection<GitHubIssue> Issues { get; } = new ObservableCollection<GitHubIssue>();
        public ObservableCollection<GitHubIssue> OpenIssues { get; } = new ObservableCollection<GitHubIssue>();
        public ObservableCollection<GitHubIssue> ClosedIssues { get; } = new ObservableCollection<GitHubIssue>();

        private IssueViewModel _selectedIssue;
        public IssueViewModel SelectedIssue
        {
            get { return _selectedIssue; }
            set
            {
                _selectedIssue = value;
                value?.FillCommentsAsync();
            }
        }

    }
}
