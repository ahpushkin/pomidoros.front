using System.ComponentModel;
using Android.Content;
using Android.Views;
using Plugin.CurrentActivity;
using Pomidoros.View.Base;
using UISampleApp.Droid.Renderers.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(BaseContentPageRenderer))]
namespace UISampleApp.Droid.Renderers.Pages
{
    public class BaseContentPageRenderer : PageRenderer
    {
        public BaseContentPageRenderer(
            Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateStatusBar();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (BaseContentPage.StatusBarColorProperty.PropertyName == e.PropertyName)
            {
                UpdateStatusBar();
            }
        }

        #endregion

        #region -- Protected implementation --

        protected virtual void UpdateStatusBar()
        {
            var page = Element as BaseContentPage;

            UpdateStatusBarColor(page.StatusBarColor);
            UpdateStatusbarIconStyle(page.Icons);
        }

        protected virtual void UpdateStatusbarIconStyle(IconsMode mode)
        {
            switch (mode)
            {
                case IconsMode.Default:
                case IconsMode.Light:
                    CrossCurrentActivity.Current.Activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Visible;
                    break;
                case IconsMode.Dark:
                    CrossCurrentActivity.Current.Activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                    break;
                default:
                    throw new InvalidEnumArgumentException("Incorrect status bar icon mode exception throw (unhandled mode)");
            }
        }

        protected virtual void UpdateStatusBarColor(Color color)
        {
            var page = Element as BaseContentPage;

            CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(color.ToAndroid());
        }

        #endregion

    }
}