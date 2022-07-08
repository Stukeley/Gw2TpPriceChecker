namespace Gw2TpPriceChecker.UI.Code
{
	public static class Comparer
	{
		public static bool Compare(int buyValue, int sellValue, int checkedValue, string comparisonType)
		{
			if (string.IsNullOrWhiteSpace(comparisonType))
			{
				return false;
			}
			
			switch (comparisonType)
			{
				case ">B":
					return buyValue > checkedValue;
				
				case ">S":
					return sellValue > checkedValue;
				
				case "<B":
					return buyValue < checkedValue;
				
				case "<S":
					return sellValue < checkedValue;
			}

			return false;
		}
	}
}