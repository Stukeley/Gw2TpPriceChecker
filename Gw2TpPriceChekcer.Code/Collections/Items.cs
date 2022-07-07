using System;
using System.Collections.Generic;

namespace Gw2TpPriceChecker.Code.Collections;

public static class Items
{
	public static Dictionary<int, string> ItemNames { get; } = new Dictionary<int, string>();
	
	public static void Dispose()
	{
		ItemNames.Clear();
	}
}
