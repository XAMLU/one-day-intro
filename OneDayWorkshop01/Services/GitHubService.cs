using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubUwpDemo1;
using GitHubUwpDemo1.Models;
using Windows.Security.Authentication.Web;
using Windows.Foundation;
using System.Runtime.CompilerServices;

namespace OneDayWorkshop01.Services
{
    class GitHubService
    {
        private SettingsService _settingsService;

        public GitHubService()
        {
            _settingsService = new SettingsService();
        }

        static GitHubService()
        {
            _gitHubClient = new GitHubClient();
        }

        static GitHubClient _gitHubClient;

        public GitHubUser GetUser()
        {
            ThrowIfNotAuthenticated();
            return _gitHubClient.GetUserDetails();
        }

        public IEnumerable<(Owner Owner, string Repository)> SearchRepositories(string query)
        {
            ThrowIfNotAuthenticated();
            return _gitHubClient.SearchRespositories(query).items.Select(x => (x.owner, x.name));
        }

        public GitHubRepository GetRepository(Owner owner, string repository)
        {
            ThrowIfNotAuthenticated();
            return _gitHubClient.GetRepository(owner, repository);
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
            set => _isAuthenticated = value;
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
                    await _gitHubClient.GetTokenFromCode(code);
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
