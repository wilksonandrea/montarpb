using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Plugin.Core.Enums;

namespace Plugin.Core.Utility
{
	// Token: 0x0200002B RID: 43
	public static class CountryDetector
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00015FBC File Offset: 0x000141BC
		static CountryDetector()
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00016090 File Offset: 0x00014290
		public static CountryFlags GetCountryByIp(string ipAddress)
		{
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
				string text = "http://ip-api.com/json/" + ipAddress + "?fields=status,country";
				using (JsonDocument jsonDocument = JsonDocument.Parse(CountryDetector.httpClient_0.GetStringAsync(text).Result, default(JsonDocumentOptions)))
				{
					JsonElement rootElement = jsonDocument.RootElement;
					JsonElement jsonElement;
					JsonElement jsonElement2;
					if (rootElement.TryGetProperty("status", ref jsonElement) && jsonElement.GetString() == "success" && rootElement.TryGetProperty("country", ref jsonElement2))
					{
						string @string = jsonElement2.GetString();
						CountryFlags countryFlags;
						if (CountryDetector.dictionary_0.TryGetValue(@string, out countryFlags))
						{
							return countryFlags;
						}
						Console.WriteLine("[CountryDetector] Connected: '" + @string + "' IP: " + ipAddress);
						return CountryFlags.None;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("[CountryDetector] Error processing IP " + ipAddress + ": " + ex.Message);
			}
			return CountryFlags.None;
		}

		// Token: 0x0400008A RID: 138
		private static readonly HttpClient httpClient_0 = new HttpClient();

		// Token: 0x0400008B RID: 139
		private static readonly Dictionary<string, CountryFlags> dictionary_0 = new Dictionary<string, CountryFlags>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Peru",
				CountryFlags.Peru
			},
			{
				"Venezuela",
				CountryFlags.Venezuela
			},
			{
				"Bolivia",
				CountryFlags.Bolivia
			},
			{
				"Ecuador",
				CountryFlags.Ecuador
			},
			{
				"United States",
				CountryFlags.US
			},
			{
				"Brazil",
				CountryFlags.Brazil
			},
			{
				"Argentina",
				CountryFlags.Argentina
			},
			{
				"Chile",
				CountryFlags.Chile
			},
			{
				"Colombia",
				CountryFlags.Colombia
			},
			{
				"Spain",
				CountryFlags.Spain
			},
			{
				"Mexico",
				CountryFlags.Mexico
			},
			{
				"Sweden",
				CountryFlags.Sweden
			},
			{
				"Indonesia",
				CountryFlags.Indonesia
			},
			{
				"Kazakhstan",
				CountryFlags.Kazakhstan
			}
		};
	}
}
