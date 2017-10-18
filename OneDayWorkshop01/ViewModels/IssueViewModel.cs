using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDayWorkshop01.Services;
using XamlU.Demo.GitHubLibrary.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

namespace OneDayWorkshop01.ViewModels
{
    class IssueViewModel : ObservableObject
    {
        private MessageService _messageService;
        private GitHubService _githubService;
        private SettingsService _settingsService;

        public IssueViewModel(GitHubIssue issue)
        {
            _messageService = new MessageService();
            _githubService = new GitHubService();
            _settingsService = new SettingsService();
            GitHubIssue = issue;
        }

        public async Task FillCommentsAsync()
        {
            if (Comments.Count == 0)
            {
                await RefillCommentsAsync();
            }
        }

        public async Task RefillCommentsAsync()
        {
            Comments.Clear();
            var repo = _settingsService.DefaultRepository;
            var comments = await _githubService.GetCommentsAsync(repo, GitHubIssue.number);
            foreach (var item in comments)
            {
                Comments.Add(item);
            }
        }

        public GitHubIssue GitHubIssue { get; set; }

        public ObservableCollection<GitHubComment> Comments { get; } = new ObservableCollection<GitHubComment>();

        private RelayCommand _refillCommentsCommand;
        private RelayCommand RefillCommentsCommand
        {
            get => this._refillCommentsCommand ?? (this._refillCommentsCommand = new RelayCommand(RefillCommandExecute()));
        }
        private Action RefillCommandExecute()
        {
            return async () => await RefillCommentsAsync();
        }
    }
}
