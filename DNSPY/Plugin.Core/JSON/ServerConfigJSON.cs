using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.JSON
{
	// Token: 0x020000A9 RID: 169
	public class ServerConfigJSON
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x0001F8CC File Offset: 0x0001DACC
		public static void Load()
		{
			string text = "Data/ServerConfig.json";
			if (File.Exists(text))
			{
				ServerConfigJSON.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Server Configs", ServerConfigJSON.Configs.Count), LoggerType.Info, null);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000066A7 File Offset: 0x000048A7
		public static void Reload()
		{
			ServerConfigJSON.Configs.Clear();
			ServerConfigJSON.Load();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001F924 File Offset: 0x0001DB24
		public static ServerConfig GetConfig(int ConfigId)
		{
			List<ServerConfig> configs = ServerConfigJSON.Configs;
			ServerConfig serverConfig2;
			lock (configs)
			{
				foreach (ServerConfig serverConfig in ServerConfigJSON.Configs)
				{
					if (serverConfig.ConfigId == ConfigId)
					{
						return serverConfig;
					}
				}
				serverConfig2 = null;
			}
			return serverConfig2;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
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
							foreach (JsonElement jsonElement in JsonDocument.Parse(streamReader.ReadToEnd(), default(JsonDocumentOptions)).RootElement.GetProperty("Server").EnumerateArray())
							{
								int num = int.Parse(jsonElement.GetProperty("ConfigId").GetString());
								if (num == 0)
								{
									CLogger.Print(string.Format("Invalid Config Id: {0}", num), LoggerType.Warning, null);
									return;
								}
								ServerConfig serverConfig = new ServerConfig
								{
									ConfigId = num,
									OnlyGM = bool.Parse(jsonElement.GetProperty("ChannelOnlyGM").GetString()),
									Missions = bool.Parse(jsonElement.GetProperty("EnableMissions").GetString()),
									AccessUFL = bool.Parse(jsonElement.GetProperty("AccessUFL").GetString()),
									UserFileList = jsonElement.GetProperty("UserFileList").GetString(),
									ClientVersion = jsonElement.GetProperty("ClientVersion").GetString(),
									GiftSystem = bool.Parse(jsonElement.GetProperty("EnableGiftSystem").GetString()),
									EnableClan = bool.Parse(jsonElement.GetProperty("EnableClan").GetString()),
									EnableTicket = bool.Parse(jsonElement.GetProperty("EnableTicket").GetString()),
									EnablePlaytime = bool.Parse(jsonElement.GetProperty("EnablePlaytime").GetString()),
									EnableTags = bool.Parse(jsonElement.GetProperty("EnableTags").GetString()),
									EnableBlood = bool.Parse(jsonElement.GetProperty("EnableBlood").GetString()),
									ExitURL = jsonElement.GetProperty("ExitURL").GetString(),
									ShopURL = jsonElement.GetProperty("ShopURL").GetString(),
									OfficialSteam = jsonElement.GetProperty("OfficialSteam").GetString(),
									OfficialBanner = jsonElement.GetProperty("OfficialBanner").GetString(),
									OfficialBannerEnabled = bool.Parse(jsonElement.GetProperty("OfficialBannerEnabled").GetString()),
									OfficialAddress = jsonElement.GetProperty("OfficialAddress").GetString(),
									ChatAnnounceColor = int.Parse(jsonElement.GetProperty("ChatAnnoucementColor").GetString()),
									ChannelAnnounceColor = int.Parse(jsonElement.GetProperty("ChannelAnnoucementColor").GetString()),
									ChatAnnounce = jsonElement.GetProperty("ChatAnnountcement").GetString(),
									ChannelAnnounce = jsonElement.GetProperty("ChannelAnnouncement").GetString(),
									Showroom = ComDiv.ParseEnum<ShowroomView>(jsonElement.GetProperty("Showroom").GetString())
								};
								ServerConfigJSON.Configs.Add(serverConfig);
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

		// Token: 0x06000804 RID: 2052 RVA: 0x00002116 File Offset: 0x00000316
		public ServerConfigJSON()
		{
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000066B8 File Offset: 0x000048B8
		// Note: this type is marked as 'beforefieldinit'.
		static ServerConfigJSON()
		{
		}

		// Token: 0x04000395 RID: 917
		public static List<ServerConfig> Configs = new List<ServerConfig>();
	}
}
