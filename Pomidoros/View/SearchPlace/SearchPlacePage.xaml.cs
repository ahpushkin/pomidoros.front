using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomidoros.View.SearchPlace
{
    public partial class SearchPlacePage : ContentPage
    {
        //main value
        public static readonly BindableProperty FocusOriginCommandProperty =
           BindableProperty.Create(nameof(FocusOriginCommand), typeof(ICommand), typeof(SearchPlacePage), null, BindingMode.TwoWay);

        public ICommand FocusOriginCommand
        {
            get { return (ICommand)GetValue(FocusOriginCommandProperty); }
            set { SetValue(FocusOriginCommandProperty, value); }
        }
        //init all componet
        //drwa main ui
        public SearchPlacePage()
        {
            InitializeComponent();
        }
        //change values on apperning
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                FocusOriginCommand = new Command(OnOriginFocus);
            }
        }
        //focus
        void OnOriginFocus()
        {
            destinationEntry.Focus();
        }
    }
}
