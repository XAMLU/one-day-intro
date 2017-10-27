using GalaSoft.MvvmLight.Command;
using System.Linq;
using GitHubBrowser.Services;
using GitHubBrowser.Controls;
using System;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GitHubBrowser.ViewModels
{
    public class CreateIssuePageViewModel : ObservableObject
    {
        private GitHubService _githubService;
        private NavigationService _navigationService;

        public CreateIssuePageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled
                || Windows.ApplicationModel.DesignMode.DesignMode2Enabled)
            {
                return;
            }
            _githubService = new GitHubService();
            _navigationService = new NavigationService();
        }

        private ValidationProperty<string> _title;
        public ValidationProperty<string> Title
        {
            get => this._title ?? (this._title = new ValidationProperty<string>
                    (string.Empty, ValidateTitle, AfterValidation));
        }

        ValidationProperty<string> _body;
        public ValidationProperty<string> Body
        {
            get => this._body ?? (this._body = new ValidationProperty<string>
                    (string.Empty, ValidateBody, AfterValidation));
        }

        private RelayCommand _createCommand;
        public RelayCommand CreateCommand
        {
            get => _createCommand ?? (_createCommand = new RelayCommand(CreateExecute, CreateCanExcute));
        }
        private async void CreateExecute()
        {
            await _githubService.CreateIssueAsync(Title.Value, Body.Value);
            _navigationService.Navigate(typeof(Views.IssuesPage));
        }
        private bool CreateCanExcute()
        {
            return (Title.IsValid && Body.IsValid);
        }

        private void ValidateTitle(string v, ObservableCollection<string> e)
        {
            e.Clear();
            if (string.IsNullOrEmpty(v))
            {
                e.Add("Value is required.");
            }
            else if (v.Length < 5)
            {
                e.Add("Value must be more than 5 characters.");
            }
            else if (v.Length > 25)
            {
                e.Add("Value must be less than 25 characters.");
            }
        }

        private void ValidateBody(string v, ObservableCollection<string> e)
        {
            e.Clear();
            if (string.IsNullOrEmpty(v))
            {
                e.Add("Value is required.");
            }
            else if (v.Length < 5)
            {
                e.Add("Value must be more than 5 characters.");
            }
        }

        private void AfterValidation()
            => CreateCommand.RaiseCanExecuteChanged();

    }
}
