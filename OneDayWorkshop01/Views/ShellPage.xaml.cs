using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using OneDayWorkshop01.Messages;
using OneDayWorkshop01.Services;
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
using System.ComponentModel;

namespace OneDayWorkshop01.Views
{
    public sealed partial class ShellPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MessageService _messageService;

        public ShellPage()
        {
            InitializeComponent();
            _messageService = new MessageService();
            _messageService.SubscribeToIsAuthenticatedChanged(this, HandleAuthChanged);
        }

        private void HandleAuthChanged(IsAuthenticatedChangedMessage message)
        {
            EnableNavigation = message.IsAuthenticated;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableNavigation)));
        }

        public bool EnableNavigation { get; set; } = false;
    }
}
