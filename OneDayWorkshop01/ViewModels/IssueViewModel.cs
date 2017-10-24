using System;
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

        public IssueViewModel(GitHubIssue issue)
        {
            _messageService = new MessageService();
            _githubService = new GitHubService();
            GitHubIssue = issue;
        }

        public async Task FillCommentsAsync()
        {
            if (Comments.Count != 0)
            {
                return;
            }
            Comments.Clear();
            var comments = await _githubService.GetCommentsAsync(GitHubIssue.number);
            foreach (var item in comments)
            {
                Comments.Add(item);
            }
        }

        public GitHubIssue GitHubIssue { get; set; }

        public ObservableCollection<GitHubComment> Comments { get; } = new ObservableCollection<GitHubComment>();
    }
}
