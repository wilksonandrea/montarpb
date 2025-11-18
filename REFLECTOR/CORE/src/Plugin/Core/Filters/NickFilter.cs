namespace Plugin.Core.Filters
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class NickFilter
    {
        public static List<string> Filters = new List<string>();

        public static void Load()
        {
            string path = "Config/Filters/Nicks.txt";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Filters.Count} Nick Filters", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Filters.Clear();
            Load();
        }

        private static void smethod_0(string string_0)
        {
            try
            {
                using (StreamReader reader = new StreamReader(string_0))
                {
                    while (true)
                    {
                        string item = reader.ReadLine();
                        if (item == null)
                        {
                            reader.Close();
                            break;
                        }
                        Filters.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("Filter: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

