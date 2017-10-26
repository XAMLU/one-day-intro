using GitHubBrowser.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GitHubBrowser.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MessageService _messageService;

        public ShellPage()
        {
            this.InitializeComponent();
            _messageService = new MessageService();
            _messageService.SubscribeToGitHubStatusChanged(this, HandleAuthChanged);
        }

        private void HandleAuthChanged(Messages.GitHubStatusChangedMessage message)
        {
            EnableNavigation = message.AllowNavigation;
        }

        private bool _enableNavigation = false;

        public bool EnableNavigation
        {
            get => _enableNavigation;
            set
            {
                _enableNavigation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableNavigation)));
            }
        }

    }
}
