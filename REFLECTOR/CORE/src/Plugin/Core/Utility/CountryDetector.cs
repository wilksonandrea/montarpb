namespace Plugin.Core.Utility
{
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;

    public static class CountryDetector
    {
        private static readonly HttpClient httpClient_0 = new HttpClient();
        private static readonly Dictionary<string, CountryFlags> dictionary_0;

        static CountryDetector()
        {
            Dictionary<string, CountryFlags> dictionary1 = new Dictionary<string, CountryFlags>(StringComparer.OrdinalIgnoreCase);
            dictionary1.Add("Peru", CountryFlags.Peru);
            dictionary1.Add("Venezuela", CountryFlags.Venezuela);
            dictionary1.Add("Bolivia", CountryFlags.Bolivia);
            dictionary1.Add("Ecuador", CountryFlags.Ecuador);
            dictionary1.Add("United States", CountryFlags.US);
            dictionary1.Add("Brazil", CountryFlags.Brazil);
            dictionary1.Add("Argentina", CountryFlags.Argentina);
            dictionary1.Add("Chile", CountryFlags.Chile);
            dictionary1.Add("Colombia", CountryFlags.Colombia);
            dictionary1.Add("Spain", CountryFlags.Spain);
            dictionary1.Add("Mexico", CountryFlags.Mexico);
            dictionary1.Add("Sweden", CountryFlags.Sweden);
            dictionary1.Add("Indonesia", CountryFlags.Indonesia);
            dictionary1.Add("Kazakhstan", CountryFlags.Kazakhstan);
            dictionary_0 = dictionary1;
        }

        public static CountryFlags GetCountryByIp(string ipAddress)
        {
            if (!string.IsNullOrEmpty(ipAddress))
            {
                if (ipAddress == "127.0.0.1")
                {
                    return CountryFlags.None;
                }
                try
                {
                    string requestUri = "http://ip-api.com/json/" + ipAddress + "?fields=status,country";
                    JsonDocumentOptions options = new JsonDocumentOptions();
                    using (JsonDocument document = JsonDocument.Parse(httpClient_0.GetStringAsync(requestUri).Result, options))
                    {
                        JsonElement element2;
                        JsonElement element3;
                        JsonElement rootElement = document.RootElement;
                        if ((rootElement.TryGetProperty("status", out element2) && (element2.GetString() == "success")) && rootElement.TryGetProperty("country", out element3))
                        {
                            CountryFlags flags;
                            CountryFlags none;
                            string key = element3.GetString();
                            if (dictionary_0.TryGetValue(key, out flags))
                            {
                                none = flags;
                            }
                            else
                            {
                                Console.WriteLine("[CountryDetector] Connected: '" + key + "' IP: " + ipAddress);
                                none = CountryFlags.None;
                            }
                            return none;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("[CountryDetector] Error processing IP " + ipAddress + ": " + exception.Message);
                }
            }
            return CountryFlags.None;
        }
    }
}

