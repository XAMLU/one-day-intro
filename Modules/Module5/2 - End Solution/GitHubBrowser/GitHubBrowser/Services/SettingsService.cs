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

        public string GithubClientId { get; } = "b90b7f8a5a016cf2e7d2";

        public string GithubSecret { get; } = "0a6a881836a1b92b22f1919b8488af900b78c688";

        public string DefaultRepository
        {
            get => _settings[nameof(DefaultRepository)] as string;
            set => _settings[nameof(DefaultRepository)] = value;
        }
    }
}
