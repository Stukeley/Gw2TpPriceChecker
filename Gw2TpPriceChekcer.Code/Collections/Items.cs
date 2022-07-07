namespace Gw2TpPriceChecker.Code.Collections;

public static class Items
{
	public static Dictionary<int, string> ItemNames { get; } = new Dictionary<int, string>();
	
	
	public static string FindNameByUserInput(string userInput)
	{
		// Create a collection of items, where each element contains the original name and the lowercase name with symbols removed.
		var itemNames = ItemNames.Values
			.Select(x => new {Name = x, NameLowerCase = new string(x.ToLowerInvariant().Where(c=>char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)).ToArray())}).ToList();
		var userInputLowerCase = userInput.ToLowerInvariant();

		string bestMatch;

		// If the user types in the exact name, return it.
		if ((bestMatch = itemNames.FirstOrDefault(x=>x.NameLowerCase == userInputLowerCase)?.Name) != null)
		{
			return bestMatch;
		}

		int dimX = itemNames.Select(x => x.NameLowerCase).Max(y => y.Length);
		int dimY = userInputLowerCase.Length;

		// Pre-allocate the array once, instead of allocating it 20+ thousand times.
		var d = new int[dimX + 1, dimY + 1];

		// Otherwise, look for the best match (using Levenshtein distance).
		bestMatch = itemNames
			.Select(x => new { Name = x.Name, Distance = ComputeLevenshteinDistance(x.NameLowerCase, userInputLowerCase, d) })
			.OrderBy(x => x.Distance).FirstOrDefault()
			?.Name;

		return bestMatch;
	}

	private static int ComputeLevenshteinDistance(string s, string t, int[,] d)
	{
		int n = s.Length;
		int m = t.Length;
		
		Array.Clear(d, 0, d.Length);

		// Step 1
		if (n == 0)
		{
			return m;
		}

		if (m == 0)
		{
			return n;
		}

		// Step 2
		for (int i = 0; i <= n; d[i, 0] = i++)
		{
		}

		for (int j = 0; j <= m; d[0, j] = j++)
		{
		}

		// Step 3
		for (int i = 1; i <= n; i++)
		{
			//Step 4
			for (int j = 1; j <= m; j++)
			{
				// Step 5
				int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

				// Step 6
				d[i, j] = Math.Min(
					Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
					d[i - 1, j - 1] + cost);
			}
		}
		// Step 7
		return d[n, m];
	}
	
	public static void Dispose()
	{
		ItemNames.Clear();
	}
}
