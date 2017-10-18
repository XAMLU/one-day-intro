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
    class IssueViewModel: ObservableObject
    {
        private MessageService _messageService;
        private GitHubService _githubService;

        public IssueViewModel(GitHubIssue issue)
        {
            _messageService = new MessageService();
            _githubService = new GitHubService();
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
            var items = await _githubService.GetCommentsAsync(GitHubIssue);
        }

        public GitHubIssue GitHubIssue { get; set; }

        public ObservableCollection<object> Comments { get; } = new ObservableCollection<object>();

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
