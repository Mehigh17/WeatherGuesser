using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Imaging.Converters;
using WeatherGuesser.Interfaces;

namespace WeatherGuesser.Services
{
	public class StandardNormalizeService : INormalizeService
	{

		private const int StandardImageSize = 128;

		private readonly ImageToArray _converter;

		public StandardNormalizeService()
		{
			_converter = new ImageToArray(0.0, 1.0);
		}

		/// <summary>
		/// Returns the the gray scale array of the standardized image reduced and cropped to 128x128 pixels.
		/// </summary>
		/// <param name="srcImage">Source image</param>
		/// <returns></returns>
		public double[] GetImage(Image srcImage)
		{
			var size = srcImage.Width > srcImage.Height ? srcImage.Height : srcImage.Width;
			var cropRectangle = new RectangleF(0f, 0f, size, size);
			using (var croppedImage = new Bitmap(size, size))
			{
				using (var g = Graphics.FromImage(croppedImage))
				{
					g.DrawImage(srcImage, srcRect: cropRectangle, destRect: cropRectangle, srcUnit: GraphicsUnit.Pixel);
				}

				using (var finalImage = new Bitmap(croppedImage, new Size(StandardImageSize, StandardImageSize)))
				{
					_converter.Convert(finalImage, out double[] imageArray);
					srcImage.Dispose();

					return imageArray;
				}
			}
		}

	}
}
