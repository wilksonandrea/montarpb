using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Plugin.Core.Enums;

namespace Plugin.Core.Utility;

public static class CountryDetector
{
	private static readonly HttpClient httpClient_0;

	private static readonly Dictionary<string, CountryFlags> dictionary_0;

	static CountryDetector()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		httpClient_0 = new HttpClient();
		dictionary_0 = new Dictionary<string, CountryFlags>(StringComparer.OrdinalIgnoreCase)
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

	public static CountryFlags GetCountryByIp(string ipAddress)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
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
			JsonDocument val = JsonDocument.Parse(httpClient_0.GetStringAsync(text).Result, default(JsonDocumentOptions));
			try
			{
				JsonElement rootElement = val.RootElement;
				JsonElement val2 = default(JsonElement);
				JsonElement val3 = default(JsonElement);
				if (((JsonElement)(ref rootElement)).TryGetProperty("status", ref val2) && ((JsonElement)(ref val2)).GetString() == "success" && ((JsonElement)(ref rootElement)).TryGetProperty("country", ref val3))
				{
					string @string = ((JsonElement)(ref val3)).GetString();
					if (dictionary_0.TryGetValue(@string, out var value))
					{
						return value;
					}
					Console.WriteLine("[CountryDetector] Connected: '" + @string + "' IP: " + ipAddress);
					return CountryFlags.None;
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("[CountryDetector] Error processing IP " + ipAddress + ": " + ex.Message);
		}
		return CountryFlags.None;
	}
}
