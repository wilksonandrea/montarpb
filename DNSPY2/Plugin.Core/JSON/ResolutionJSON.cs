using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.JSON
{
	// Token: 0x020000A8 RID: 168
	public class ResolutionJSON
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x0001F684 File Offset: 0x0001D884
		public static void Load()
		{
			string text = "Data/DisplayRes.json";
			if (File.Exists(text))
			{
				ResolutionJSON.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Allowed DRAR", ResolutionJSON.ARS.Count), LoggerType.Info, null);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0000668A File Offset: 0x0000488A
		public static void Reload()
		{
			ResolutionJSON.ARS.Clear();
			ResolutionJSON.Load();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		public static string GetDisplay(string R)
		{
			List<string> ars = ResolutionJSON.ARS;
			string text2;
			lock (ars)
			{
				foreach (string text in ResolutionJSON.ARS)
				{
					string[] array = ComDiv.SplitObjects(R, "x");
					if (text == ComDiv.AspectRatio(int.Parse(array[0]), int.Parse(array[1])))
					{
						return text;
					}
				}
				text2 = "Invalid";
			}
			return text2;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001F78C File Offset: 0x0001D98C
		private static void smethod_0(string string_0)
		{
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
				}
				else
				{
					try
					{
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
						{
							foreach (JsonElement jsonElement in JsonDocument.Parse(streamReader.ReadToEnd(), default(JsonDocumentOptions)).RootElement.GetProperty("Resolution").EnumerateArray())
							{
								string @string = jsonElement.GetProperty("AspectRatio").GetString();
								ResolutionJSON.ARS.Add(@string);
							}
							streamReader.Dispose();
							streamReader.Close();
						}
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00002116 File Offset: 0x00000316
		public ResolutionJSON()
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0000669B File Offset: 0x0000489B
		// Note: this type is marked as 'beforefieldinit'.
		static ResolutionJSON()
		{
		}

		// Token: 0x04000394 RID: 916
		public static List<string> ARS = new List<string>();
	}
}
