using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Gw2TpPriceChecker.Code.Converters;

namespace Gw2TpPriceChecker.UI.Windows
{
	public partial class PriceInputWindow : Window
	{
		public static int TotalPriceInCopper;
		
		public PriceInputWindow()
		{
			InitializeComponent();
		}

		private void PriceInputWindow_OnClosing(object sender, CancelEventArgs e)
		{
			try
			{
				var priceInGold = string.IsNullOrEmpty(GoldPriceBox.Text) ? 0 : int.Parse(GoldPriceBox.Text);
				var priceInSilver = string.IsNullOrEmpty(SilverPriceBox.Text) ? 0 : int.Parse(SilverPriceBox.Text);
				var priceInCopper = string.IsNullOrEmpty(CopperPriceBox.Text) ? 0 : int.Parse(CopperPriceBox.Text);

				if (priceInCopper > 99)
				{
					priceInSilver += priceInCopper / 100;
					priceInCopper %= 100;
				}
				
				if (priceInSilver > 99)
				{
					priceInGold += priceInSilver / 100;
					priceInSilver %= 100;
				}

				TotalPriceInCopper = ItemPriceConverter.ConvertToTotalPrice(priceInGold, priceInSilver, priceInCopper);
			}
			catch (Exception)
			{
				TotalPriceInCopper = 0;
			}
		}

		private void PriceInputWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				this.Close();
			}
		}
	}
}