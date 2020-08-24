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
        }
       
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 5,
                StrokeCap = SKStrokeCap.Butt,
                PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 20)
            };

            SKPath path = new SKPath();

            canvas.DrawPath(path, paint);
        }
    }
}
