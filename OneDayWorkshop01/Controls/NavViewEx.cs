using System;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneDayWorkshop01.Controls
{
    public partial class NavProperties : DependencyObject
    {
        public static Type GetPageType(NavigationViewItem obj)
            => (Type)obj.GetValue(PageTypeProperty);
        public static void SetPageType(NavigationViewItem obj, Type value)
            => obj.SetValue(PageTypeProperty, value);
        public static readonly DependencyProperty PageTypeProperty =
            DependencyProperty.RegisterAttached("PageType", typeof(Type),
                typeof(NavProperties), new PropertyMetadata(null));
    }

    public class NavViewEx : NavigationView
    {
        Frame _frame;

        public Type SettingsPageType { get; set; }

        public NavViewEx()
        {
            Content = _frame = new Frame();
            _frame.Navigated += Frame_Navigated;
            ItemInvoked += NavViewEx_ItemInvoked;
            SystemNavigationManager.GetForCurrentView().BackRequested += ShellPage_BackRequested;
            RegisterPropertyChangedCallback(IsPaneOpenProperty, IsPaneOpenChanged);
        }

        private void IsPaneOpenChanged(DependencyObject sender, DependencyProperty dp)
        {
            foreach (var item in MenuItems.OfType<NavigationViewItemHeader>())
            {
                item.Visibility = IsPaneOpen ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void NavViewEx_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
                SelectedItem = SettingsItem;
            else
                SelectedItem = Find(args.InvokedItem.ToString());
        }

        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
            => SelectedItem = Find(e.SourcePageType);

        private void ShellPage_BackRequested(object sender, BackRequestedEventArgs e)
            => _frame.GoBack();

        NavigationViewItem Find(string content)
            => MenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => x.Content.Equals(content));

        NavigationViewItem Find(Type type)
            => MenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => type.Equals(x.GetValue(NavProperties.PageTypeProperty)));

        public virtual void Navigate(Frame frame, Type type)
            => frame.Navigate(type);

        public new object SelectedItem
        {
            set
            {
                if (value == SettingsItem)
                {
                    Navigate(_frame, SettingsPageType);
                    base.SelectedItem = value;
                    _frame.BackStack.Clear();
                }
                else if (value is NavigationViewItem i && i != null)
                {
                    Navigate(_frame, i.GetValue(NavProperties.PageTypeProperty) as Type);
                    base.SelectedItem = value;
                    _frame.BackStack.Clear();
                }
                UpdateBackButton();
            }
        }

        private void UpdateBackButton()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                (_frame.CanGoBack) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
