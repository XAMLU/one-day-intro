using System.Linq;
using OneDayWorkshop01.Services;
using System.Collections.ObjectModel;

namespace OneDayWorkshop01.ViewModels
{
    class IssuesPageViewModel
    {
        private GitHubService _githubService;

        public IssuesPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled
               || Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                return;
            }
            _githubService = new GitHubService();
            RefreshIssues();
        }

        private async void RefreshIssues()
        {
            OpenIssues.Clear();
            ClosedIssues.Clear();

            var items = await _githubService.GetIssuesAsync();

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
