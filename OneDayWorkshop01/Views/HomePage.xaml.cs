﻿using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OneDayWorkshop01.Services;
using OneDayWorkshop01.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OneDayWorkshop01.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            Loaded += HomePage_Loaded;
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoginAsync();
        }

        private async void Login_Clicked(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoginAsync();
        }
    }
}
