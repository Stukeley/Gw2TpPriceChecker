namespace Gw2TpPriceChecker.UI.Code
{
	public static class Comparer
	{
		public static bool Compare(int actualValue, int setValue, char comparisonType)
		{
			switch (comparisonType)
			{
				case '=':
					return actualValue == setValue;
				case '>':
					return actualValue > setValue;
				case '<':
					return actualValue < setValue;
			}

			return false;
		}
	}
}