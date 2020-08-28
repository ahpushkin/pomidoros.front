﻿using System;
using Pomidoros.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pomidoros
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //set launch page
            //pls dont change this code)
            MainPage = new NavigationPage(new MorePage());
        }
        //OnStart method
        protected override void OnStart()
        {
        }
        //OnSleep method
        protected override void OnSleep()
        {
        }
        //OnResum method
        protected override void OnResume()
        {
        }
    }
}
