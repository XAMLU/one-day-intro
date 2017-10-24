using System;
using GalaSoft.MvvmLight.Messaging;

namespace OneDayWorkshop01.Services
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

        public void SubscribeToGithubStatusChanged(object sender, Action<Messages.GithubStatusChangedMessage> action)
        {
            _messenger.Register(sender, action);
        }

        public void SendGithubStatusChanged()
        {
            _messenger.Send(new Messages.GithubStatusChangedMessage
            {
                IsAuthenticated = _githubService.IsAuthenticated,
                DefaultRepository = _settingService.DefaultRepository,
            });
        }
    }
}
