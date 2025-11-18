namespace Plugin.Core.JSON
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;

    public class ResolutionJSON
    {
        public static List<string> ARS = new List<string>();

        public static string GetDisplay(string R)
        {
            List<string> aRS = ARS;
            lock (aRS)
            {
                using (List<string>.Enumerator enumerator = ARS.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        string current = enumerator.Current;
                        string[] strArray = ComDiv.SplitObjects(R, "x");
                        if (current == ComDiv.AspectRatio(int.Parse(strArray[0]), int.Parse(strArray[1])))
                        {
                            return current;
                        }
                    }
                }
                return "Invalid";
            }
        }

        public static void Load()
        {
            string path = "Data/DisplayRes.json";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {ARS.Count} Allowed DRAR", LoggerType.Info, null);
        }

        public static void Reload()
        {
            ARS.Clear();
            Load();
        }

        private static void smethod_0(string string_0)
        {
            using (FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
            {
                if (stream.Length == 0)
                {
                    CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
                }
                else
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            JsonDocumentOptions options = new JsonDocumentOptions();
                            JsonElement property = JsonDocument.Parse(reader.ReadToEnd(), options).RootElement.GetProperty("Resolution");
                            foreach (JsonElement element2 in property.EnumerateArray())
                            {
                                string item = element2.GetProperty("AspectRatio").GetString();
                                ARS.Add(item);
                            }
                            reader.Dispose();
                            reader.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                stream.Dispose();
                stream.Close();
            }
        }
    }
}

