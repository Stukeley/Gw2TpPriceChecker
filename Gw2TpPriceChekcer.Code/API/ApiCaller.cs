using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Gw2TpPriceChecker.Code.Models;

namespace Gw2TpPriceChecker.Code.API;

public class ApiCaller
{
	public static async Task<ItemPrice> CheckAndReturnCurrentPrice(string itemId)
	{
		var url = @"https://api.guildwars2.com/v2/commerce/prices/" + itemId;

		using (var client = new HttpClient())
		{
			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();

				var responseObj = JsonSerializer.Deserialize<ItemPrice>(json);

				return responseObj;
			}
			else
			{
				throw new Exception("Invalid response from the API.");
			}
		}
	}

	public static async Task<byte[]> GetItemIconById(int itemId)
	{
		var url = @"https://api.guildwars2.com/v2/items/" + itemId;
		
		using (var client = new HttpClient())
		{
			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();

				var responseObj = JsonSerializer.Deserialize<Item>(json);

				var iconUrl = responseObj.icon;
				
				var iconBytes = await client.GetByteArrayAsync(iconUrl);

				return iconBytes;
			}
			else
			{
				throw new Exception("Invalid response from the API.");
			}
		}
	}
}