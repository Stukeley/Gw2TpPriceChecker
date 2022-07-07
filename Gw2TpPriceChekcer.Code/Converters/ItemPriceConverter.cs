namespace Gw2TpPriceChecker.Code.Converters;

public static class ItemPriceConverter
{
	public static int ConvertToTotalPrice(int gold, int silver, int copper)
	{
		return gold * 10000 + silver * 100 + copper;
	}

	public static (int Gold, int Silver, int Copper) ConvertToGoldSilverCopperPrice(int totalPrice)
	{
		int gold = totalPrice / 10000;
		int silver = (totalPrice % 10000) / 100;
		int copper = totalPrice % 100;

		return (gold, silver, copper);
	}
}
