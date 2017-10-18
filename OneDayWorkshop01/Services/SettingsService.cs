using Windows.Foundation.Collections;
using Windows.Storage;
using XamlU.Demo.GitHubLibrary.Models;

namespace OneDayWorkshop01.Services
{
    class SettingsService
    {
        private IPropertySet _settings;

        public SettingsService()
        {
            _settings = ApplicationData.Current.LocalSettings.Values;
        }

        public string GithubClientId { get; } = "284b459693e719db2c0f";
        public string GithubSecret { get; } = "9a244fcff7f2b4afac57b962dac2c062b15dcef8";
        public string DefaultRepository
        {
            get => _settings[nameof(DefaultRepository)] as string;
            set => _settings[nameof(DefaultRepository)] = value;
        }
    }
}
