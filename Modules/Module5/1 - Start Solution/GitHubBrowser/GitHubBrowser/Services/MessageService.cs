using System;
using GalaSoft.MvvmLight.Messaging;

namespace GitHubBrowser.Services
{
    public class MessageService
    {
        private static Messenger _messenger;
        private static GitHubService _githubService;
        private static SettingsService _settingService;

        static MessageService()
        {
            _messenger = new Messenger();
            _githubService = new GitHubService();
            _settingService = new SettingsService();
        }

        public void SubscribeToGitHubStatusChanged(object sender, Action<Messages.GitHubStatusChangedMessage> action)
        {
            _messenger.Register(sender, action);
        }

        public void SendGitHubStatusChanged()
        {
            _messenger.Send(new Messages.GitHubStatusChangedMessage
            {
                IsAuthenticated = _githubService.IsAuthenticated,
                DefaultRepository = _settingService.DefaultRepository,
            });
        }
    }
}
