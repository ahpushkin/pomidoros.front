using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class LocationPage : ContentPage
    {
        public static readonly BindableProperty CalculateCommandProperty =
        BindableProperty.Create(nameof(CalculateCommand), typeof(ICommand), typeof(MainPage), null, BindingMode.TwoWay);

        public ICommand CalculateCommand
        {
            get { return (ICommand)GetValue(CalculateCommandProperty); }
            set { SetValue(CalculateCommandProperty, value); }
        }

        public static readonly BindableProperty UpdateCommandProperty =
          BindableProperty.Create(nameof(UpdateCommand), typeof(ICommand), typeof(MainPage), null, BindingMode.TwoWay);

        public ICommand UpdateCommand
        {
            get { return (ICommand)GetValue(UpdateCommandProperty); }
            set { SetValue(UpdateCommandProperty, value); }
        }

        public LocationPage()
        {
            InitializeComponent();
        }
    }
}
