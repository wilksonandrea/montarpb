using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace Plugin.Core.Filters
{
	public static class NickFilter
	{
		public static List<string> Filters;

		static NickFilter()
		{
			NickFilter.Filters = new List<string>();
		}

		public static void Load()
		{
			string str = "Config/Filters/Nicks.txt";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				NickFilter.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Nick Filters", NickFilter.Filters.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			NickFilter.Filters.Clear();
			NickFilter.Load();
		}

		private static void smethod_0(string string_0)
		{
			try
			{
				using (StreamReader streamReader = new StreamReader(string_0))
				{
					while (true)
					{
						string str = streamReader.ReadLine();
						string str1 = str;
						if (str == null)
						{
							break;
						}
						NickFilter.Filters.Add(str1);
					}
					streamReader.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Filter: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}