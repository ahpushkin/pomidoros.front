using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Pomidoros.Controls
{
    public class Circle
    {
        private readonly Func<SKImageInfo, SKPoint> _centerfunc;

        public Circle(float redius, Func<SKImageInfo, SKPoint> centerfunc)
        {
            _centerfunc = centerfunc;
            Redius = redius;
        }
        public SKPoint Center { get; set; }
        public float Redius { get; set; }
        public SKRect Rect => new SKRect(Center.X - Redius, Center.Y - Redius, Center.X + Redius, Center.Y + Redius);

        public class ProgressDrawer
        {

            public ProgressDrawer(SKCanvasView canvas, Circle circle, Func<float> progress, float strokeWidth, SKColor progressColor, SKColor foregroundColor)
            {
                canvas.PaintSurface += (sender, args) =>
                {
                    circle.CalculateCenter(args.Info);
                    args.Surface.Canvas.Clear();
                    DrawCircle(args.Surface.Canvas, circle, strokeWidth, progressColor);
                    DrawArc(args.Surface.Canvas, circle, progress, strokeWidth, foregroundColor);

                };
            }

            private void DrawCircle(SKCanvas canvas, Circle circle, float strokewidth, SKColor color)
            {
                canvas.DrawCircle(circle.Center, circle.Redius,
                    new SKPaint()
                    {
                        StrokeWidth = strokewidth,
                        Color = color,
                        IsStroke = true
                    });

            }

            private void DrawArc(SKCanvas canvas, Circle circle, Func<float> progress, float strokewidth, SKColor color)
            {
                var angle = progress.Invoke() * 3.6f;
                canvas.DrawArc(circle.Rect, 270, angle, false,
                    new SKPaint() { StrokeWidth = strokewidth, Color = color, IsStroke = true });
            }

        }

        public void CalculateCenter(SKImageInfo argsInfo)
        {
            Center = _centerfunc.Invoke(argsInfo);
        }
    }
    public class ProgressDrawer
    {

        public ProgressDrawer(SKCanvasView canvas, Circle circle, Func<float> progress, float strokeWidth, SKColor progressColor, SKColor foregroundColor)
        {
            canvas.PaintSurface += (sender, args) =>
            {
                circle.CalculateCenter(args.Info);
                args.Surface.Canvas.Clear();
                DrawCircle(args.Surface.Canvas, circle, strokeWidth, progressColor);
                DrawArc(args.Surface.Canvas, circle, progress, strokeWidth, foregroundColor);

            };
        }

        private void DrawCircle(SKCanvas canvas, Circle circle, float strokewidth, SKColor color)
        {
            canvas.DrawCircle(circle.Center, circle.Redius,
                new SKPaint()
                {
                    StrokeWidth = strokewidth,
                    Color = color,
                    IsStroke = true
                });

        }

        private void DrawArc(SKCanvas canvas, Circle circle, Func<float> progress, float strokewidth, SKColor color)
        {
            var angle = progress.Invoke() * 3.6f;
            canvas.DrawArc(circle.Rect, 270, angle, false,
                new SKPaint() { StrokeWidth = strokewidth, Color = color, IsStroke = true });
        }

    }
}
