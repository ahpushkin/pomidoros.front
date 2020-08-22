using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class MorePage : ContentPage
    {

        public Color LineColor
        {
            get; set;
        } = Color.Black;

        public float DashSize
        {
            get; set;
        } = 20;

        public float WhiteSize
        {
            get; set;
        } = 20;

        public float Phase
        {
            get; set;
        } = 0;

        public MorePage()
        {
            InitializeComponent();

            var orderslist = new List<string>();

            orderslist.Add("Паперони спайс × 2шт");
            orderslist.Add("Четыре мяса × 1 шт");
            orderslist.Add("Pepsi 1литр  × 1 шт");

            orders.ItemsSource = orderslist;
        }
       
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
    }
}
