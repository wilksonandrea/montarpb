using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Plugin.Core.XML
{
	public class SeasonPassXML
	{
		public readonly static List<BattlePassModel> Seasons;

		static SeasonPassXML()
		{
			SeasonPassXML.Seasons = new List<BattlePassModel>();
		}

		public SeasonPassXML()
		{
		}

		public static void Load()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(Directory.GetCurrentDirectory(), "\\Data\\Seasons"));
			if (!directoryInfo.Exists)
			{
				return;
			}
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				try
				{
					SeasonPassXML.smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					CLogger.Print(exception.Message, LoggerType.Error, exception);
				}
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Season Challenges", SeasonPassXML.Seasons.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			SeasonPassXML.Seasons.Clear();
			SeasonPassXML.Load();
		}

		private static void smethod_0(int int_0)
		{
			string str = string.Format("Data/Seasons/{0}.xml", int_0);
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
		}
	}
}