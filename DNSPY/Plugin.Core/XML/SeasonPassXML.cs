using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x0200001B RID: 27
	public class SeasonPassXML
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00012AC0 File Offset: 0x00010CC0
		public static void Load()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\Seasons");
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				try
				{
					SeasonPassXML.smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Season Challenges", SeasonPassXML.Seasons.Count), LoggerType.Info, null);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000027D4 File Offset: 0x000009D4
		public static void Reload()
		{
			SeasonPassXML.Seasons.Clear();
			SeasonPassXML.Load();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00012B6C File Offset: 0x00010D6C
		private static void smethod_0(int int_0)
		{
			string text = string.Format("Data/Seasons/{0}.xml", int_0);
			if (!File.Exists(text))
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002116 File Offset: 0x00000316
		public SeasonPassXML()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000027E5 File Offset: 0x000009E5
		// Note: this type is marked as 'beforefieldinit'.
		static SeasonPassXML()
		{
		}

		// Token: 0x04000065 RID: 101
		public static readonly List<BattlePassModel> Seasons = new List<BattlePassModel>();
	}
}
