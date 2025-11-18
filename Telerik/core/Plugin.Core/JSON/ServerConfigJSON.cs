using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Plugin.Core.JSON
{
	public class ServerConfigJSON
	{
		public static List<ServerConfig> Configs;

		static ServerConfigJSON()
		{
			ServerConfigJSON.Configs = new List<ServerConfig>();
		}

		public ServerConfigJSON()
		{
		}

		public static ServerConfig GetConfig(int ConfigId)
		{
			ServerConfig serverConfig;
			lock (ServerConfigJSON.Configs)
			{
				foreach (ServerConfig config in ServerConfigJSON.Configs)
				{
					if (config.ConfigId != ConfigId)
					{
						continue;
					}
					serverConfig = config;
					return serverConfig;
				}
				serverConfig = null;
			}
			return serverConfig;
		}

		public static void Load()
		{
			string str = "Data/ServerConfig.json";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				ServerConfigJSON.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Server Configs", ServerConfigJSON.Configs.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			ServerConfigJSON.Configs.Clear();
			ServerConfigJSON.Load();
		}

		private static void smethod_0(string string_0)
		{
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
			{
				if (fileStream.Length != 0)
				{
					try
					{
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
						{
							string end = streamReader.ReadToEnd();
							JsonDocumentOptions jsonDocumentOption = new JsonDocumentOptions();
							JsonElement rootElement = JsonDocument.Parse(end, jsonDocumentOption).get_RootElement();
							rootElement = rootElement.GetProperty("Server");
							foreach (JsonElement jsonElement in rootElement.EnumerateArray())
							{
								rootElement = jsonElement.GetProperty("ConfigId");
								int ınt32 = int.Parse(rootElement.GetString());
								if (ınt32 == 0)
								{
									CLogger.Print(string.Format("Invalid Config Id: {0}", ınt32), LoggerType.Warning, null);
									return;
								}
								else
								{
									ServerConfig serverConfig = new ServerConfig()
									{
										ConfigId = ınt32
									};
									rootElement = jsonElement.GetProperty("ChannelOnlyGM");
									serverConfig.OnlyGM = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("EnableMissions");
									serverConfig.Missions = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("AccessUFL");
									serverConfig.AccessUFL = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("UserFileList");
									serverConfig.UserFileList = rootElement.GetString();
									rootElement = jsonElement.GetProperty("ClientVersion");
									serverConfig.ClientVersion = rootElement.GetString();
									rootElement = jsonElement.GetProperty("EnableGiftSystem");
									serverConfig.GiftSystem = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("EnableClan");
									serverConfig.EnableClan = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("EnableTicket");
									serverConfig.EnableTicket = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("EnablePlaytime");
									serverConfig.EnablePlaytime = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("EnableTags");
									serverConfig.EnableTags = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("EnableBlood");
									serverConfig.EnableBlood = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("ExitURL");
									serverConfig.ExitURL = rootElement.GetString();
									rootElement = jsonElement.GetProperty("ShopURL");
									serverConfig.ShopURL = rootElement.GetString();
									rootElement = jsonElement.GetProperty("OfficialSteam");
									serverConfig.OfficialSteam = rootElement.GetString();
									rootElement = jsonElement.GetProperty("OfficialBanner");
									serverConfig.OfficialBanner = rootElement.GetString();
									rootElement = jsonElement.GetProperty("OfficialBannerEnabled");
									serverConfig.OfficialBannerEnabled = bool.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("OfficialAddress");
									serverConfig.OfficialAddress = rootElement.GetString();
									rootElement = jsonElement.GetProperty("ChatAnnoucementColor");
									serverConfig.ChatAnnounceColor = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("ChannelAnnoucementColor");
									serverConfig.ChannelAnnounceColor = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("ChatAnnountcement");
									serverConfig.ChatAnnounce = rootElement.GetString();
									rootElement = jsonElement.GetProperty("ChannelAnnouncement");
									serverConfig.ChannelAnnounce = rootElement.GetString();
									rootElement = jsonElement.GetProperty("Showroom");
									serverConfig.Showroom = ComDiv.ParseEnum<ShowroomView>(rootElement.GetString());
									ServerConfigJSON.Configs.Add(serverConfig);
								}
							}
							streamReader.Dispose();
							streamReader.Close();
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
					}
				}
				else
				{
					CLogger.Print(string.Concat("File is empty: ", string_0), LoggerType.Warning, null);
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}
	}
}