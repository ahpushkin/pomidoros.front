using Xamarin.Forms;

namespace Pomidoros.View.Base
{
    public class BaseContentPage : ContentPage
    {
        public static BindableProperty HasNavigationBarProperty = BindableProperty.Create(
            nameof(HasNavigationBar),
            typeof(bool),
            typeof(BaseContentPage),
            true,
            propertyChanged: OnHasNavigationBarPropertyChanged);

        public bool HasNavigationBar
        {
            get { return (bool)GetValue(HasNavigationBarProperty); }
            set { SetValue(HasNavigationBarProperty, value); }
        }

        public static BindableProperty StatusBarColorProperty = BindableProperty.Create(
            nameof(StatusBarColor),
            typeof(Color),
            typeof(BaseContentPage));

        public Color StatusBarColor
        {
            get { return (Color)GetValue(StatusBarColorProperty); }
            set { SetValue(StatusBarColorProperty, value); }
        }

        public static BindableProperty IconsProperty = BindableProperty.Create(
            nameof(Icons),
            typeof(IconsMode),
            typeof(BaseContentPage));

        public IconsMode Icons
        {
            get { return (IconsMode)GetValue(IconsProperty); }
            set { SetValue(IconsProperty, value); }
        }

        private static void OnHasNavigationBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            NavigationPage.SetHasNavigationBar(bindable, (bool)newValue);
        }
    }

    public enum IconsMode
    {
        Default = 0,
        Light,
        Dark
    }
}