using Windows.Foundation.Collections;
using Windows.Storage;

namespace GitHubBrowser.Services
{
    class SettingsService
    {
        private IPropertySet _settings;

        public SettingsService()
        {
            _settings = ApplicationData.Current.LocalSettings.Values;
        }

        public string GithubClientId { get; } = "<your client id>";

        public string GithubSecret { get; } = "<your secret>";

        public string DefaultRepository
        {
            get => _settings[nameof(DefaultRepository)] as string;
            set => _settings[nameof(DefaultRepository)] = value;
        }
    }
}
