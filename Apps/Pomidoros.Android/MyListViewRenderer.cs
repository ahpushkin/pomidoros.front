using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Pomidoros.Droid
{
    public class MyListViewRenderer : ListViewRenderer
    {
        Context ctx;
        public MyListViewRenderer(Context context) : base(context)
        {
            ctx = context;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //var listView = Control as Android.Widget.ListView;
                Drawable drawable = Resources.GetDrawable(Resource.Drawable.DashedLines);

                Control.DividerHeight = 20;
                Control.Divider = drawable;
                Control.SetLayerType(LayerType.Software, null);
            }
        }
    }
}
