using System;
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
                UpdateStatusBarColor();
                UpdateStatusbarIconStyle();
                e.NewElement.Appearing += OnPageAppearing;
            }

            if (e.OldElement != null)
                e.NewElement.Appearing -= OnPageAppearing;
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            UpdateStatusBarColor();
            UpdateStatusbarIconStyle();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (BaseContentPage.StatusBarColorProperty.PropertyName == e.PropertyName)
                UpdateStatusBarColor();

            if (BaseContentPage.IconsProperty.PropertyName == e.PropertyName)
                UpdateStatusbarIconStyle();
        }

        #endregion

        #region -- Protected implementation --

        protected virtual void UpdateStatusbarIconStyle()
        {
            if (Element is BaseContentPage page)
            {
                switch (page.Icons)
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
        }

        protected virtual void UpdateStatusBarColor()
        {
            if (Element is BaseContentPage page) 
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(page.StatusBarColor.ToAndroid());
        }

        #endregion

    }
}