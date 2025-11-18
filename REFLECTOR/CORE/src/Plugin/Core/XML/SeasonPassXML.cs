namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class SeasonPassXML
    {
        public static readonly List<BattlePassModel> Seasons = new List<BattlePassModel>();

        public static void Load()
        {
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Data\Seasons");
            if (info.Exists)
            {
                foreach (FileInfo info2 in info.GetFiles())
                {
                    try
                    {
                        smethod_0(int.Parse(info2.Name.Substring(0, info2.Name.Length - 4)));
                    }
                    catch (Exception exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                CLogger.Print($"Plugin Loaded: {Seasons.Count} Season Challenges", LoggerType.Info, null);
            }
        }

        public static void Reload()
        {
            Seasons.Clear();
            Load();
        }

        private static void smethod_0(int int_0)
        {
            string path = $"Data/Seasons/{int_0}.xml";
            if (!File.Exists(path))
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }
    }
}

