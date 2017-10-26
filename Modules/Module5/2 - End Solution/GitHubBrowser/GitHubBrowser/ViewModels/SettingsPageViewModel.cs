using GalaSoft.MvvmLight.Command;
using GitHubBrowser.Services;
using Windows.ApplicationModel;

namespace GitHubBrowser.ViewModels
{
    class SettingsPageViewModel
    {
        private SettingsService _settingService;
        private GitHubService _githubService;
        private MessageService _messageService;

        public SettingsPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled
                || Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                return;
            }
            _settingService = new SettingsService();
            _githubService = new GitHubService();
            _messageService = new MessageService();
        }

        public RelayCommand _clearDefaultCommand;
        public RelayCommand ClearDefaultCommand
        {
            get => _clearDefaultCommand ?? (_clearDefaultCommand = new RelayCommand(ClearDefaultExecute, ClearDefaultCanExecute));
        }
        private void ClearDefaultExecute()
        {
            _settingService.DefaultRepository = null;
            ClearDefaultCommand.RaiseCanExecuteChanged();
            _messageService.SendGitHubStatusChanged();
        }
        private bool ClearDefaultCanExecute()
        {
            return !string.IsNullOrEmpty(_settingService.DefaultRepository);
        }

        public string Version
        {
            get
            {
                var p = Package.Current;
                var v = p.Id.Version;
                return $"{p.DisplayName} v{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }
    }
}
