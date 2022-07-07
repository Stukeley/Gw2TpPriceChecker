namespace Gw2TpPriceChecker.Code.Models;

public class ItemPrice
{
	public int id { get; set; }
	public bool whitelisted { get; set; }
	public Buys buys { get; set; }
	public Sells sells { get; set; }
}