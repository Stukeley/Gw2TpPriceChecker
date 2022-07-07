namespace Gw2TpPriceChecker.Code.Models;

public class Details
{
	public string type { get; set; }
	public string weight_class { get; set; }
	public int defense { get; set; }
	public object[] infusion_slots { get; set; }
	public float attribute_adjustment { get; set; }
	public Infix_Upgrade infix_upgrade { get; set; }
	public string secondary_suffix_item_id { get; set; }
}