using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OneDayWorkshop01.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
            Loaded += ShellPage_Loaded;
            NavigationFrame.Navigated += NavigationFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += ShellPage_BackRequested;
        }

        private void ShellPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainNavigationView.SelectedItem = HomeButton;
        }

        private void ShellPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            NavigationFrame.GoBack();
        }

        private void NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (NavigationFrame.Content)
            {
                case HomePage p:
                    MainNavigationView.SelectedItem = HomeButton;
                    break;
                case CodePage p:
                    MainNavigationView.SelectedItem = CodeButton;
                    break;
                case PullPage p:
                    MainNavigationView.SelectedItem = PullButton;
                    break;
                case SettingsPage p:
                    MainNavigationView.SelectedItem = MainNavigationView.SettingsItem;
                    break;
                case IssuesPage p:
                    MainNavigationView.SelectedItem = IssuesButton;
                    break;
                default:
                    UpdateBackButton();
                    break;
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected && !(NavigationFrame.Content is SettingsPage))
            {
                NavigationFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                switch ((args.SelectedItem as NavigationViewItem).Name)
                {
                    case nameof(HomeButton) when !(NavigationFrame.Content is HomePage):
                        NavigationFrame.Navigate(typeof(HomePage));
                        break;
                    case nameof(CodeButton) when !(NavigationFrame.Content is CodePage):
                        NavigationFrame.Navigate(typeof(CodePage));
                        break;
                    case nameof(IssuesButton) when !(NavigationFrame.Content is IssuesPage):
                        NavigationFrame.Navigate(typeof(IssuesPage));
                        break;
                    case nameof(PullButton) when !(NavigationFrame.Content is PullPage):
                        NavigationFrame.Navigate(typeof(PullPage));
                        break;
                }
            }
            NavigationFrame.BackStack.Clear();
            UpdateBackButton();
        }

        private void UpdateBackButton()
        {
            if (NavigationFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                    = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                    = AppViewBackButtonVisibility.Collapsed;
            }
        }
    }
}
