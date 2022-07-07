namespace Gw2TpPriceChecker.Code.Models;

public class Item
{
	public string name { get; set; }
	public string description { get; set; }
	public string type { get; set; }
	public int level { get; set; }
	public string rarity { get; set; }
	public int vendor_value { get; set; }
	public int default_skin { get; set; }
	public string[] game_types { get; set; }
	public object[] flags { get; set; }
	public object[] restrictions { get; set; }
	public int id { get; set; }
	public string chat_link { get; set; }
	public string icon { get; set; }
	public Details details { get; set; }
}