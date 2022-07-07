using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Gw2TpPriceChecker.UI.Converters;

public static class ImageConverter
{
	public static Image ConvertByteArrayToImage(byte[] bytes)
	{
		var bmp = new BitmapImage();
		
		bmp.BeginInit();
		bmp.StreamSource = new MemoryStream(bytes);
		bmp.EndInit();

		var img = new Image
		{
			Source = bmp,
			Width = 64,
			Height = 64,
			HorizontalAlignment = HorizontalAlignment.Center,
			VerticalAlignment = VerticalAlignment.Center
		};

		return img;
	}
}