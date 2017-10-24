using System;
using System.Linq;
using System.Threading.Tasks;
using XamlU.Demo.GitHubLibrary;
using XamlU.Demo.GitHubLibrary.Models;
using Windows.Security.Authentication.Web;
using Windows.Foundation;
using System.Runtime.CompilerServices;

namespace OneDayWorkshop01.Services
{
    class GitHubService
    {
        static GitHubClient _gitHubClient;

        static GitHubService()
        {
            _gitHubClient = new GitHubClient();
        }

        private SettingsService _settingsService;
        private MessageService _messageService;

        public GitHubService()
        {
            _settingsService = new SettingsService();
            _messageService = new MessageService();
        }

        public async Task<GitHubUser> GetUserAsync()
        {
            ThrowIfNotAuthenticated();
            return await _gitHubClient.GetUserDetailsAsync();
        }

        public async Task<SearchRepositoriesItem[]> SearchRepositoriesAsync(string query)
        {
            ThrowIfNotAuthenticated();
            return (await _gitHubClient.SearchRespositoriesAsync(query))?.items;
        }

        internal async Task<GitHubComment[]> GetCommentsAsync(int issueId)
        {
            ThrowIfNotAuthenticated();
            var repo = _settingsService.DefaultRepository;
            return await _gitHubClient.GetRepositoryIssueCommentsAsync(repo, issueId);
        }

        public async Task<GitHubIssue[]> GetIssuesAsync()
        {
            ThrowIfNotAuthenticated();
            var repo = _settingsService.DefaultRepository;
            return (await _gitHubClient.GetRepositoryIssuesAsync(repo)).Issues;
        }

        public async Task<GitHubCreateIssueResponse> CreateIssueAsync(string title, string body)
        {
            ThrowIfNotAuthenticated();
            var repo = _settingsService.DefaultRepository;
            var issue = new GitHubCreateIssue
            {
                title = title,
                body = body,
                assignee = string.Empty,
                labels = new string[] { }
            };
            return await _gitHubClient.PostRepositoryIssueAsync(repo, issue);
        }

        private void ThrowIfNotAuthenticated([CallerMemberName]string source = null)
        {
            if (!IsAuthenticated)
            {
                throw new Exception($"Not authenticated.\r\n{nameof(GitHubService)}.{source}");
            }
        }

        static bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                _isAuthenticated = value;
                _messageService.SendGithubStatusChanged();
            }
        }

        public async Task<bool> AuthenticateAsync(bool silent = false)
        {
            if (IsAuthenticated)
                return true;

            var callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            var requestUri = new Uri($"https://github.com/login/oauth/authorize?" +
                                     $"client_id={_settingsService.GithubClientId}" +
                                     $"&redirect_uri={Uri.EscapeUriString(callbackUri.ToString())}" +
                                     $"&scope=read_stream repo&display=popup&response_type=token");

            WebAuthenticationResult webAuthResult;
            if (silent)
            {
                webAuthResult = await WebAuthenticationBroker.AuthenticateSilentlyAsync(
                    requestUri, WebAuthenticationOptions.None);
            }
            else
            {
                webAuthResult = await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None, requestUri);
            }

            switch (webAuthResult.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    var resultAsUri = new Uri(webAuthResult.ResponseData);
                    var names = new WwwFormUrlDecoder(resultAsUri.Query);
                    var code = names.Single(x => x.Name == "code").Value;
                    await _gitHubClient.GetTokenFromCodeAsync(_settingsService.GithubClientId, _settingsService.GithubSecret, code);
                    return IsAuthenticated = true;
                case WebAuthenticationStatus.ErrorHttp:
                    throw new Exception(webAuthResult.ResponseErrorDetail.ToString());
                case WebAuthenticationStatus.UserCancel:
                default:
                    return false;
            }
        }
    }
}
