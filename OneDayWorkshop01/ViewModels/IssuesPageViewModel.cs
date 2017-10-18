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
                RefreshIssues();
            }
        }

        private async void RefreshIssues()
        {
            OpenIssues.Clear();
            ClosedIssues.Clear();

            var repo = _settingsService.DefaultRepository;
            var items = await _githubService.GetIssuesAsync(repo);

            foreach (var item in items
                .Where(x => x.state.Equals("open"))
                .Select(x => new IssueViewModel(x)))
            {
                OpenIssues.Add(item);
            }
            foreach (var item in items
                .Where(x => !x.state.Equals("open"))
                .Select(x => new IssueViewModel(x)))
            {
                ClosedIssues.Add(item);
            }
        }

        public ObservableCollection<IssueViewModel> OpenIssues { get; } = new ObservableCollection<IssueViewModel>();
        public ObservableCollection<IssueViewModel> ClosedIssues { get; } = new ObservableCollection<IssueViewModel>();

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
