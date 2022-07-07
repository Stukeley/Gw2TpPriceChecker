using System;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Gw2TpPriceChecker.Code.API;
using Gw2TpPriceChecker.Code.Collections;
using Gw2TpPriceChecker.Code.Converters;
using Gw2TpPriceChecker.Code.Models;
using Gw2TpPriceChecker.Code.Parsers;
using Gw2TpPriceChecker.UI.Code;
using Gw2TpPriceChecker.UI.Windows;

namespace Gw2TpPriceChecker.UI
{
	public partial class MainWindow : Window
	{
		private DispatcherTimer _timer;
		private int _priceThreshold;
		private char _priceComparisonType;
		private int _intervalInSeconds = 30;

		public MainWindow()
		{
			InitializeComponent();
			
			ItemNamesParser.LoadItemNames();

			_timer = null;
		}

		private void StartStopButton_Click(object sender, RoutedEventArgs e)
		{
			if (_timer is null)
			{
				_timer = new DispatcherTimer();
				_timer.Interval = TimeSpan.FromSeconds(_intervalInSeconds);
				_timer.Tick += Timer_Tick;
			}

			if (!_timer.IsEnabled)
			{
				string itemName;

				try
				{
					if (string.IsNullOrEmpty(ItemNameBox.Text))
					{
						itemName = Items.ItemNames[int.Parse(ItemIdBox.Text)];
					}
					else
					{
						itemName = ItemNameBox.Text;
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Item of the given ID does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				StartStopButton.Content = "Stop";
				
				// Disable ID, name and threshold input.
				ItemIdBox.IsEnabled = false;
				ItemNameBox.IsEnabled = false;
				IntervalBox.IsEnabled = false;
				ItemPriceThresholdBox.IsEnabled = false;
				ItemPriceComparisonTypeBox.IsEnabled = false;

				if (!string.IsNullOrWhiteSpace(ItemPriceThresholdBox.Text))
				{
					_ = int.TryParse(ItemPriceThresholdBox.Text, out _priceThreshold);
					_ = char.TryParse(ItemPriceComparisonTypeBox.Text, out _priceComparisonType);
				}
				
				ItemNameBox.Text = itemName;
				Timer_Tick(this, EventArgs.Empty);
				
				_timer.Start();
				
				// Dispose of the Dictionary with item IDs and Names.
				Items.Dispose();
			}
			else
			{
				StartStopButton.Content = "Start";
				
				// Enable ID, name and threshold input again.
				ItemIdBox.IsEnabled = true;
				ItemNameBox.IsEnabled = true;
				IntervalBox.IsEnabled = true;
				ItemPriceThresholdBox.IsEnabled = true;
				ItemPriceComparisonTypeBox.IsEnabled = true;
				
				_priceThreshold = 0;
				_priceComparisonType = ' ';
				
				_timer.Stop();
				_timer = null;
				
				// Re-make the Dictionary with item IDs and Names.
				ItemNamesParser.LoadItemNames();
			}
		}
		
		private void SetOutAlert()
		{
			for (int i = 0; i < 3; i++)
			{
				SystemSounds.Exclamation.Play();
				Thread.Sleep(50);
			}
			
			Thread.Sleep(300);
			MessageBox.Show("Price is within threshold!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void UpdatePriceUI(ItemPrice currentItemPrice)
		{
			SellAndBuyPricesSeparator.Visibility = Visibility.Visible;
			BuyPricesBlock.Visibility = Visibility.Visible;
			SellPricesBlock.Visibility = Visibility.Visible;

			var buyPrices = ItemPriceConverter.ConvertToGoldSilverCopperPrice(currentItemPrice.buys.unit_price);
			var sellPrices = ItemPriceConverter.ConvertToGoldSilverCopperPrice(currentItemPrice.sells.unit_price);

			BuyPriceGold.Text = buyPrices.Gold.ToString();
			BuyPriceSilver.Text = buyPrices.Silver.ToString();
			BuyPriceCopper.Text = buyPrices.Copper.ToString();
			BuyPricesPanel.Visibility = Visibility.Visible;

			SellPriceGold.Text = sellPrices.Gold.ToString();
			SellPriceSilver.Text = sellPrices.Silver.ToString();
			SellPriceCopper.Text = sellPrices.Copper.ToString();
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

		private async void ItemNameBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			var itemName = ItemNameBox.Text.ToLower();

			var correctItemName = Items.FindNameByUserInput(itemName);
			
			var itemId = Items.ItemNames.FirstOrDefault(x=>x.Value == correctItemName).Key;

			ItemIdBox.Text = itemId.ToString();
			ItemNameBox.Text = correctItemName;

			await UpdateItemIcon();
		}

		private async void ItemIdBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			var itemId = ItemIdBox.Text.Trim();

			if (!string.IsNullOrEmpty(itemId))
			{
				_ = int.TryParse(itemId, out int itemIdInt);
				
				var itemName = Items.ItemNames.FirstOrDefault(x=>x.Key == itemIdInt).Value;

				ItemNameBox.Text = itemName;
			}

			await UpdateItemIcon();
		}

		private void IntervalBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			_ = int.TryParse(IntervalBox.Text, out int intervalInSeconds);

			_intervalInSeconds = Math.Clamp(intervalInSeconds, 30, 300);

			IntervalBox.Text = _intervalInSeconds.ToString();
		}

		private async Task UpdateItemIcon()
		{
			var itemIconBytes = await ApiCaller.GetItemIconById(int.Parse(ItemIdBox.Text));

			Application.Current.Dispatcher.Invoke(() =>
			{
				ItemIconPanel.Children.Clear();
				ItemIconPanel.Children.Add(Converters.ImageConverter.ConvertByteArrayToImage(itemIconBytes));
			});
		}

		private void ItemPriceThresholdBox_OnGotFocus(object sender, RoutedEventArgs e)
		{
			new PriceInputWindow().ShowDialog();
			
			_priceThreshold = PriceInputWindow.TotalPriceInCopper;

			ItemPriceThresholdBox.Text = _priceThreshold.ToString();

			ItemPriceThresholdBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
		}
	}
}