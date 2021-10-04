using System;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Gw2TpPriceChecker.Code;

namespace Gw2TpPriceChecker
{
	public partial class MainWindow : Window
	{
		private DispatcherTimer _timer;
		private int _priceThreshold;
		private char _priceComparisonType;

		public MainWindow()
		{
			InitializeComponent();

			_timer = new DispatcherTimer() {Interval = new TimeSpan(0,1,0)};
			_timer.Tick += Timer_Tick;
		}

		private async void StartStopButton_Click(object sender, RoutedEventArgs e)
		{
			if (!_timer.IsEnabled)
			{
				StartStopButton.Content = "Stop";
				
				// Disable ID and threshold input.
				ItemIdBox.IsEnabled = false;
				ItemPriceThresholdBox.IsEnabled = false;
				ItemPriceComparisonTypeBox.IsEnabled = false;

				if (!string.IsNullOrWhiteSpace(ItemPriceThresholdBox.Text))
				{
					_priceThreshold = int.Parse(ItemPriceThresholdBox.Text);
					_priceComparisonType = char.Parse(ItemPriceComparisonTypeBox.SelectedValue.ToString());
				}

				string itemName = await ApiCaller.GetItemNameById(ItemIdBox.Text);
				ItemNameBox.Text = itemName;
				Timer_Tick(this, EventArgs.Empty);
				
				_timer.Start();
			}
			else
			{
				StartStopButton.Content = "Start";
				
				// Enable ID and threshold input again.
				ItemIdBox.IsEnabled = true;
				ItemPriceThresholdBox.IsEnabled = true;
				ItemPriceComparisonTypeBox.IsEnabled = true;
				
				_priceThreshold = 0;
				_priceComparisonType = ' ';
				
				_timer.Stop();
			}
		}
		
		private void SetOutAlert()
		{
			for (int i = 0; i < 3; i++)
			{
				SystemSounds.Exclamation.Play();
				Thread.Sleep(1000);
			}
		}

		private void UpdatePriceUI(ItemPrice currentItemPrice)
		{
			SellAndBuyPricesSeparator.Visibility = Visibility.Visible;
			BuyPricesBlock.Visibility = Visibility.Visible;
			SellPricesBlock.Visibility = Visibility.Visible;
			
			BuyPriceGold.Text = (currentItemPrice.buys.unit_price / 10000).ToString();
			BuyPriceSilver.Text = ((currentItemPrice.buys.unit_price % 10000) / 100).ToString();
			BuyPriceCopper.Text = (currentItemPrice.buys.unit_price % 100).ToString();
			BuyPricesPanel.Visibility = Visibility.Visible;

			SellPriceGold.Text = (currentItemPrice.sells.unit_price / 10000).ToString();
			SellPriceSilver.Text = ((currentItemPrice.sells.unit_price % 10000) / 100).ToString();
			SellPriceCopper.Text = (currentItemPrice.sells.unit_price % 100).ToString();
			SellPricesPanel.Visibility = Visibility.Visible;
		}
		
		private async void Timer_Tick(object sender, EventArgs e)
		{
			string itemId = ItemIdBox.Text;

			try
			{
				var currentItemPrice = await ApiCaller.CheckAndReturnCurrentPrice(itemId);
				
				UpdatePriceUI(currentItemPrice);

				bool isThresholdEnabled = _priceComparisonType != ' ' && _priceThreshold != 0;

				// Compare price and set out an alert if the result is true. (only if comparison type is set)
				if (isThresholdEnabled && Comparer.Compare(currentItemPrice.buys.unit_price, _priceThreshold, _priceComparisonType))
				{
					SetOutAlert();
				}

				LastCheckedBlock.Text = $"Last checked:\n{DateTime.Now:HH:mm:ss}";
			}
			catch (Exception)
			{
				LastCheckedBlock.Text = "Last checked:\nERROR";
			}
		}
	}
}