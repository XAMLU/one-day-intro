using XamlU.Demo.GitHubLibrary.Models;

namespace OneDayWorkshop01.Services
{
    class SettingsService
    {
        public string GithubClientId { get; } = "284b459693e719db2c0f";
        public string GithubSecret { get; } = "9a244fcff7f2b4afac57b962dac2c062b15dcef8";
        public (Owner Owner, string Repository) DefaultRepository { get; internal set; }
    }
}
