using System;
using OneDayWorkshop01.Controls;
namespace OneDayWorkshop01.Services
{
    public class NavigationService
    {
        private static NavViewEx _nav;

        public static void Setup(NavViewEx nav)
        {
            _nav = nav;
        }

        public bool Navigate(Type type)
        {
            return _nav.Navigate(type);
        }
    }
}
