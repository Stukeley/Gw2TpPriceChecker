using System.Text.Json;
using Gw2TpPriceChecker.Code.Collections;

namespace Gw2TpPriceChecker.Code.Parsers;

public static class ItemNamesParser
{
	// items-names.json courtesy of Gw2TP: http://api.gw2tp.com/1/bulk/items-names.json
	public static void LoadItemNames()
	{
		var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "items-names.json"));

		var itemNamesList = JsonSerializer.Deserialize<List<JsonElement>>(json);

		itemNamesList.ForEach(x =>
		{
			var jsonElementAsList = x.EnumerateArray().ToList();
			Items.ItemNames.Add(jsonElementAsList[0].GetInt32(), jsonElementAsList[1].GetString().ToLower());
		});
	}
}
