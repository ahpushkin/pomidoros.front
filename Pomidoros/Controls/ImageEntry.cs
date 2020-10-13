using System;
using Xamarin.Forms;
namespace Pomidoros.Controls
{
	//class for custom entry
	public class ImageEntry : Entry
	{
		public ImageEntry()
		{
			this.HeightRequest = 50;
		}
		//main data
		//BindableProperties
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(ImageEntry), string.Empty);

		public static readonly BindableProperty LineColorProperty =
			BindableProperty.Create(nameof(LineColor), typeof(Xamarin.Forms.Color), typeof(ImageEntry), Color.White);

		public static readonly BindableProperty ImageVerticalScaleProperty =
			BindableProperty.Create(nameof(ImageVerticalScale), typeof(double), typeof(ImageEntry), 0.07);

		public static readonly BindableProperty ImageHorizontalScaleProperty =
			BindableProperty.Create(nameof(ImageHorizontalScale), typeof(double), typeof(ImageEntry), 0.07);

		public static readonly BindableProperty ImageAlignmentProperty =
			BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(ImageEntry), ImageAlignment.Left);

		//change color
		//of entry line
		public Color LineColor
		{
			get { return (Color)GetValue(LineColorProperty); }
			set { SetValue(LineColorProperty, value); }
		}

		public double ImageHorizontalScale
		{
			get { return (double)GetValue(ImageHorizontalScaleProperty); }
			set { SetValue(ImageHorizontalScaleProperty, value); }
		}

		public double ImageVerticalScale
		{
			get { return (double)GetValue(ImageVerticalScaleProperty); }
			set { SetValue(ImageVerticalScaleProperty, value); }
		}

		public string Image
		{
			get { return (string)GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}

		public ImageAlignment ImageAlignment
		{
			get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
			set { SetValue(ImageAlignmentProperty, value); }
		}
	}
	//icon location
	//enum
	public enum ImageAlignment
	{
		Left,
		Right
	}
}
