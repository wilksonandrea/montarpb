using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;

namespace Plugin.Core
{
	// Token: 0x02000006 RID: 6
	public static class Translation
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002129 File Offset: 0x00000329
		static Translation()
		{
			Translation.smethod_0();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00008B48 File Offset: 0x00006D48
		private static void smethod_0()
		{
			string text = "Config/Translate/Strings.ini";
			if (File.Exists(text))
			{
				Translation.smethod_1(text);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00008B7C File Offset: 0x00006D7C
		private static void smethod_1(string string_0)
		{
			try
			{
				using (StreamReader streamReader = new StreamReader(string_0))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						int num = text.IndexOf(" = ");
						if (num >= 0)
						{
							string text2 = text.Substring(0, num);
							string text3 = text.Substring(num + 3);
							Translation.Strings.Add(text2, text3);
						}
					}
					streamReader.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("Translation: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00008C1C File Offset: 0x00006E1C
		public static string GetLabel(string Title)
		{
			string text2;
			try
			{
				string text;
				if (Translation.Strings.TryGetValue(Title, out text))
				{
					text2 = text.Replace("\\n", '\n'.ToString());
				}
				else
				{
					text2 = Title;
				}
			}
			catch
			{
				text2 = Title;
			}
			return text2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000213A File Offset: 0x0000033A
		public static string GetLabel(string Title, params object[] Argumens)
		{
			return string.Format(Translation.GetLabel(Title), Argumens);
		}

		// Token: 0x0400004A RID: 74
		public static SortedList<string, string> Strings = new SortedList<string, string>();
	}
}
