using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;

namespace Plugin.Core.Filters
{
	// Token: 0x02000042 RID: 66
	public static class NickFilter
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00019644 File Offset: 0x00017844
		public static void Load()
		{
			string text = "Config/Filters/Nicks.txt";
			if (File.Exists(text))
			{
				NickFilter.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Nick Filters", NickFilter.Filters.Count), LoggerType.Info, null);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00003248 File Offset: 0x00001448
		public static void Reload()
		{
			NickFilter.Filters.Clear();
			NickFilter.Load();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001969C File Offset: 0x0001789C
		private static void smethod_0(string string_0)
		{
			try
			{
				using (StreamReader streamReader = new StreamReader(string_0))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						NickFilter.Filters.Add(text);
					}
					streamReader.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("Filter: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00003259 File Offset: 0x00001459
		// Note: this type is marked as 'beforefieldinit'.
		static NickFilter()
		{
		}

		// Token: 0x040000D5 RID: 213
		public static List<string> Filters = new List<string>();
	}
}
