using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gw2TpPriceChecker.Code
{
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
					throw new Exception("Invalid response from the API");
				}
			}
		}

		public static async Task<string> GetItemNameById(string itemId)
		{
			var url = @"https://api.guildwars2.com/v2/items/" + itemId;
			
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();

					var responseObj = JsonDocument.Parse(json);

					string itemName = responseObj.RootElement.GetProperty("name").GetString();
					
					return itemName;
				}
				else
				{
					throw new Exception("Invalid response from the API");
				}
			}
			
		}
	}
}