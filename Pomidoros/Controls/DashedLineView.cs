using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Pomidoros.Controls
{
    public class DashedLineView : ContentView
    {
        //Main values for color
        //Entry render
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

        //main method
        //Content
        public DashedLineView()
        {
            Content = new SKCanvasView();
            ((SKCanvasView)Content).PaintSurface += Canvas_PaintSurface;
        }
        //Render entry for Android
        //by SKiaSharp
        private void Canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = LineColor.ToSKColor(),
                StrokeWidth = HeightRequest > 0 ? (float)HeightRequest : (float)WidthRequest,
                StrokeCap = SKStrokeCap.Butt,
                PathEffect = SKPathEffect.CreateDash(new float[] { DashSize, WhiteSize }, Phase)
            };

            SKPath path = new SKPath();
            if (HeightRequest > 0)
            {
                // Horizontal
                path.MoveTo(0, 0);
                path.LineTo(info.Width, 0);
            }
            else
            {
                // Vertikal
                path.MoveTo(0, 0);
                path.LineTo(0, info.Height);
            }
            //DrawPath
            //
            canvas.DrawPath(path, paint);
        }
    }


}
