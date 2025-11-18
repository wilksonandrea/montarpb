using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Plugin.Core.Utility
{
	public static class CountryDetector
	{
		private readonly static HttpClient httpClient_0;

		private readonly static Dictionary<string, CountryFlags> dictionary_0;

		static CountryDetector()
		{
			CountryDetector.httpClient_0 = new HttpClient();
			CountryDetector.dictionary_0 = new Dictionary<string, CountryFlags>(StringComparer.OrdinalIgnoreCase)
			{
				{ "Peru", CountryFlags.Peru },
				{ "Venezuela", CountryFlags.Venezuela },
				{ "Bolivia", CountryFlags.Bolivia },
				{ "Ecuador", CountryFlags.Ecuador },
				{ "United States", CountryFlags.US },
				{ "Brazil", CountryFlags.Brazil },
				{ "Argentina", CountryFlags.Argentina },
				{ "Chile", CountryFlags.Chile },
				{ "Colombia", CountryFlags.Colombia },
				{ "Spain", CountryFlags.Spain },
				{ "Mexico", CountryFlags.Mexico },
				{ "Sweden", CountryFlags.Sweden },
				{ "Indonesia", CountryFlags.Indonesia },
				{ "Kazakhstan", CountryFlags.Kazakhstan }
			};
		}

		public static CountryFlags GetCountryByIp(string ipAddress)
		{
			JsonElement jsonElement = new JsonElement();
			JsonElement jsonElement1 = new JsonElement();
			CountryFlags countryFlag;
			CountryFlags countryFlag1;
			if (string.IsNullOrEmpty(ipAddress))
			{
				return CountryFlags.None;
			}
			if (ipAddress == "127.0.0.1")
			{
				return CountryFlags.None;
			}
			try
			{
				string str = string.Concat("http://ip-api.com/json/", ipAddress, "?fields=status,country");
				using (JsonDocument jsonDocument = JsonDocument.Parse(CountryDetector.httpClient_0.GetStringAsync(str).Result, new JsonDocumentOptions()))
				{
					JsonElement rootElement = jsonDocument.get_RootElement();
					if (rootElement.TryGetProperty("status", ref jsonElement) && jsonElement.GetString() == "success" && rootElement.TryGetProperty("country", ref jsonElement1))
					{
						string str1 = jsonElement1.GetString();
						if (!CountryDetector.dictionary_0.TryGetValue(str1, out countryFlag))
						{
							Console.WriteLine(string.Concat("[CountryDetector] Connected: '", str1, "' IP: ", ipAddress));
							countryFlag1 = CountryFlags.None;
							return countryFlag1;
						}
						else
						{
							countryFlag1 = countryFlag;
							return countryFlag1;
						}
					}
				}
				return CountryFlags.None;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				Console.WriteLine(string.Concat("[CountryDetector] Error processing IP ", ipAddress, ": ", exception.Message));
				return CountryFlags.None;
			}
			return countryFlag1;
		}
	}
}