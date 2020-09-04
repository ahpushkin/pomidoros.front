﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Pomidoros.Controls;
using Pomidoros.View.Notification;
using Pomidoros.ViewModel;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pomidoros.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BreakPage : ContentPage
    {
        SKPaintSurfaceEventArgs args;
        ProgressUtils progressUtils = new ProgressUtils();
        int dailyWorkout = 20;
        int monthlyWorkout = 900;
        int goal = 900;

        private string timerProperty;
        public string main_data;
        public int count;
        public string TimerProperty
        {
            get { return timerProperty; }
            set
            {
                timerProperty = value;
                OnPropertyChanged(nameof(TimerProperty)); // Notify that there was a change on this property
            }
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public BreakPage()
        {
            InitializeComponent();


            BindingContext = this;
            // Drawing the Radial Gauge
            initiateProgressUpdate();
        }

        async void OnCanvasViewPaintSurfaceAsync(object sender, SKPaintSurfaceEventArgs args1)

        {
            args = args1;
            await drawGaugeAsync();

        }

        // Event Handler for Switch Toggle
        void switchToggledAsync(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            initiateProgressUpdate();
        }


        void sliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (canvas != null)
            {
                // Invalidating surface due to data change
                canvas.InvalidateSurface();
            }
        }


        // Animating the Progress of Radial Gauge
        async void animateProgress(int progress)
        {
            sw_listToggle.IsEnabled = false;
            sweepAngleSlider.Value = 1;

            // Looping at data interval of 5
            for (int i = 0; i < progress; i = i + 5)
            {
                sweepAngleSlider.Value = i;
                await Task.Delay(3);
            }

            sweepAngleSlider.Value = progress;
            sw_listToggle.IsEnabled = true;

        }

        void initiateProgressUpdate()
        {
            //  if (sw_listToggle.IsToggled)
            animateProgress(progressUtils.getSweepAngle(goal, monthlyWorkout));
            //else
            //    animateProgress(progressUtils.getSweepAngle(goal / 30, dailyWorkout));
        }

        public async Task drawGaugeAsync()
        {
            // Radial Gauge Constants
            int uPadding = 30;
            int side = 500;
            int radialGaugeWidth = 25;

            // Line TextSize inside Radial Gauge
            int lineSize1 = 220;
            int lineSize2 = 70;
            int lineSize3 = 80;

            // Line Y Coordinate inside Radial Gauge
            int lineHeight1 = 100;
            int lineHeight2 = 200;
            int lineHeight3 = 300;

            // Start & End Angle for Radial Gauge
            float startAngle = -90;
            float sweepAngle = 360;

            try
            {

                // Getting Canvas Info 
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;
                progressUtils.setDevice(info.Height, info.Width);
                canvas.Clear();

                // Getting Device Specific Screen Values
                // -------------------------------------------------

                // Top Padding for Radial Gauge
                float upperPading = progressUtils.getFactoredHeight(uPadding);

                /* Coordinate Plotting for Radial Gauge
                *
                *    (X1,Y1) ------------
                *           |   (XC,YC)  |
                *           |      .     |
                *         Y |            |
                *           |            |
                *            ------------ (X2,Y2))
                *                  X
                *   
                *To fit a perfect Circle inside --> X==Y
                *       i.e It should be a Square
                */

                // Xc & Yc are center of the Circle
                int Xc = info.Width / 2;
                float Yc = progressUtils.getFactoredHeight(side);

                // X1 Y1 are lefttop cordiates of rectange
                int X1 = (int)(Xc - Yc);
                int Y1 = (int)(Yc - Yc + upperPading);

                // X2 Y2 are rightbottom cordiates of rectange
                int X2 = (int)(Xc + Yc);
                int Y2 = (int)(Yc + Yc + upperPading);

                //Loggig Screen Specific Calculated Values
                Debug.WriteLine("INFO " + info.Width + " - " + info.Height);
                Debug.WriteLine(" C : " + upperPading + "  " + info.Height);
                Debug.WriteLine(" C : " + Xc + "  " + Yc);
                Debug.WriteLine("XY : " + X1 + "  " + Y1);
                Debug.WriteLine("XY : " + X2 + "  " + Y2);

                //  Empty Gauge Styling
                SKPaint paint1 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#05c782").ToSKColor(),                   // Colour of Radial Gauge
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), // Width of Radial Gauge
                    StrokeCap = SKStrokeCap.Round                                   // Round Corners for Radial Gauge
                };

                // Filled Gauge Styling
                SKPaint paint2 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#e0dfdf").ToSKColor(),                   // Overlay Colour of Radial Gauge
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), // Overlay Width of Radial Gauge
                    StrokeCap = SKStrokeCap.Round                                   // Round Corners for Radial Gauge
                };

                // Defining boundaries for Gauge
                SKRect rect = new SKRect(X1, Y1, X2, Y2);


                //canvas.DrawRect(rect, paint1);
                //canvas.DrawOval(rect, paint1);

                // Rendering Empty Gauge
                SKPath path1 = new SKPath();
                path1.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path1, paint1);

                // Rendering Filled Gauge
                SKPath path2 = new SKPath();
                path2.AddArc(rect, startAngle, (float)sweepAngleSlider.Value);
                canvas.DrawPath(path2, paint2);

                //---------------- Drawing Text Over Gauge ---------------------------

                // Achieved Minutes
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#676a69");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize1);
                    skPaint.Typeface = SKTypeface.FromFamilyName(
                                        "Arial",
                                        SKFontStyleWeight.Bold,
                                        SKFontStyleWidth.Normal,
                                        SKFontStyleSlant.Upright);

                    // Drawing Achieved Minutes Over Radial Gauge
                    if (sw_listToggle.IsToggled)
                        canvas.DrawText(monthlyWorkout + "", Xc, Yc + progressUtils.getFactoredHeight(lineHeight1), skPaint);
                    else
                        canvas.DrawText(dailyWorkout + "", Xc, Yc + progressUtils.getFactoredHeight(lineHeight1), skPaint);
                }

                // Achieved Minutes Text Styling
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#676a69");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize2);
                    canvas.DrawText("Minutes", Xc, Yc + progressUtils.getFactoredHeight(lineHeight2), skPaint);
                }

                // Goal Minutes Text Styling
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#e2797a");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize3);

                    // Drawing Text Over Radial Gauge
                    if (sw_listToggle.IsToggled)
                        canvas.DrawText("Goal " + goal + " Min", Xc, Yc + progressUtils.getFactoredHeight(lineHeight3), skPaint);
                    else
                    {
                        canvas.DrawText("Goal " + goal / 30 + " Min", Xc, Yc + progressUtils.getFactoredHeight(lineHeight3), skPaint);
                    }
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }


        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            count = 900;
            UpdateTime();
            while (true)
            {
                await Task.Delay(1000);
                count--;
                UpdateTime();

                if (count == 0)
                {
                    await this.Navigation.PushAsync(new OverPage());
                    break;
                }
            }
        }
        void UpdateTime()
        {
            TimeSpan time = TimeSpan.FromSeconds(count);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            string str = time.ToString(@"mm\:ss");
            main_data = str;
            TimerProperty = main_data;
        }
        
    }
}
