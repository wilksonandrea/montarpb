using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;

namespace Plugin.Core;

public static class Translation
{
	public static SortedList<string, string> Strings;

	static Translation()
	{
		Strings = new SortedList<string, string>();
		smethod_0();
	}

	private static void smethod_0()
	{
		string text = "Config/Translate/Strings.ini";
		if (File.Exists(text))
		{
			smethod_1(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	private static void smethod_1(string string_0)
	{
		try
		{
			using StreamReader streamReader = new StreamReader(string_0);
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				int num = text.IndexOf(" = ");
				if (num >= 0)
				{
					string key = text.Substring(0, num);
					string value = text.Substring(num + 3);
					Strings.Add(key, value);
				}
			}
			streamReader.Close();
		}
		catch (Exception ex)
		{
			CLogger.Print("Translation: " + ex.Message, LoggerType.Error, ex);
		}
	}

	public static string GetLabel(string Title)
	{
		try
		{
			if (Strings.TryGetValue(Title, out var value))
			{
				return value.Replace("\\n", '\n'.ToString());
			}
			return Title;
		}
		catch
		{
			return Title;
		}
	}

	public static string GetLabel(string Title, params object[] Argumens)
	{
		return string.Format(GetLabel(Title), Argumens);
	}
}
