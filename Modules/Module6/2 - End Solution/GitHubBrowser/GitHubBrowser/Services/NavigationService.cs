using GitHubBrowser.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubBrowser.Services
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
