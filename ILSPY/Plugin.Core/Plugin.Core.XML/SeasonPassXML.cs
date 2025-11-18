using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class SeasonPassXML
{
	public static readonly List<BattlePassModel> Seasons = new List<BattlePassModel>();

	public static void Load()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\Seasons");
		if (!directoryInfo.Exists)
		{
			return;
		}
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			try
			{
				smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
		CLogger.Print($"Plugin Loaded: {Seasons.Count} Season Challenges", LoggerType.Info);
	}

	public static void Reload()
	{
		Seasons.Clear();
		Load();
	}

	private static void smethod_0(int int_0)
	{
		string text = $"Data/Seasons/{int_0}.xml";
		if (!File.Exists(text))
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}
}
