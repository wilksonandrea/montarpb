using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;

namespace Plugin.Core.Filters;

public static class NickFilter
{
	public static List<string> Filters = new List<string>();

	public static void Load()
	{
		string text = "Config/Filters/Nicks.txt";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Filters.Count} Nick Filters", LoggerType.Info);
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
			using StreamReader streamReader = new StreamReader(string_0);
			string item;
			while ((item = streamReader.ReadLine()) != null)
			{
				Filters.Add(item);
			}
			streamReader.Close();
		}
		catch (Exception ex)
		{
			CLogger.Print("Filter: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
