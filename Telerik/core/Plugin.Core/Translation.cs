using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace Plugin.Core
{
	public static class Translation
	{
		public static SortedList<string, string> Strings;

		static Translation()
		{
			Translation.Strings = new SortedList<string, string>();
			Translation.smethod_0();
		}

		public static string GetLabel(string Title)
		{
			string str;
			string title;
			try
			{
				title = (!Translation.Strings.TryGetValue(Title, out str) ? Title : str.Replace("\\n", '\n'.ToString()));
			}
			catch
			{
				title = Title;
			}
			return title;
		}

		public static string GetLabel(string Title, params object[] Argumens)
		{
			return string.Format(Translation.GetLabel(Title), Argumens);
		}

		private static void smethod_0()
		{
			string str = "Config/Translate/Strings.ini";
			if (File.Exists(str))
			{
				Translation.smethod_1(str);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		private static void smethod_1(string string_0)
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
						int ınt32 = str1.IndexOf(" = ");
						if (ınt32 >= 0)
						{
							string str2 = str1.Substring(0, ınt32);
							string str3 = str1.Substring(ınt32 + 3);
							Translation.Strings.Add(str2, str3);
						}
					}
					streamReader.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Translation: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}