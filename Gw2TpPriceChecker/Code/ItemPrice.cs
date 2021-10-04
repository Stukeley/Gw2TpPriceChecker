namespace Gw2TpPriceChecker.Code
{
	public class ItemPrice
	{
		public int id { get; set; }
		public bool whitelisted { get; set; }
		public Buys buys { get; set; }
		public Sells sells { get; set; }
	}

	public class Buys
	{
		public int quantity { get; set; }
		public int unit_price { get; set; }
	}
	
	public class Sells
	{
		public int quantity { get; set; }
		public int unit_price { get; set; }
	}
}