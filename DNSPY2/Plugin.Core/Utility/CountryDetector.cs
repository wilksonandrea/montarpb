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
                string url = "http://ip-api.com/json/" + ipAddress + "?fields=status,country";
                string json = CountryDetector.httpClient_0.GetStringAsync(url).Result;

                using (JsonDocument jsonDocument = JsonDocument.Parse(json))
                {
                    JsonElement root = jsonDocument.RootElement;

                    if (root.TryGetProperty("status", out JsonElement statusElem) &&
                        statusElem.GetString() == "success" &&
                        root.TryGetProperty("country", out JsonElement countryElem))
                    {
                        string country = countryElem.GetString();
                        if (CountryDetector.dictionary_0.TryGetValue(country, out CountryFlags flag))
                        {
                            return flag;
                        }

                        Console.WriteLine($"[CountryDetector] Connected: '{country}' IP: {ipAddress}");
                        return CountryFlags.None;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CountryDetector] Country info is null. IP : " + ipAddress +" Error: " + ex.Message);
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
