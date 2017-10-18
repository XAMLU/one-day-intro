using GalaSoft.MvvmLight.Command;
using OneDayWorkshop01.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace OneDayWorkshop01.ViewModels
{
    class SettingsPageViewModel
    {
        private SettingsService _settingService;

        public SettingsPageViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled
                || !Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                _settingService = new SettingsService();
            }
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
