using GalaSoft.MvvmLight.Command;
using GitHubBrowser.Services;
using Windows.ApplicationModel;

namespace GitHubBrowser.ViewModels
{
    class SettingsPageViewModel
    {
        private SettingsService _settingService;
        private GitHubService _githubService;

        public SettingsPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled
                || Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                return;
            }
            _settingService = new SettingsService();
            _githubService = new GitHubService();
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
