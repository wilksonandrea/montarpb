using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.JSON;

public class ServerConfigJSON
{
	public static List<ServerConfig> Configs = new List<ServerConfig>();

	public static void Load()
	{
		string text = "Data/ServerConfig.json";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Configs.Count} Server Configs", LoggerType.Info);
	}

	public static void Reload()
	{
		Configs.Clear();
		Load();
	}

	public static ServerConfig GetConfig(int ConfigId)
	{
		lock (Configs)
		{
			foreach (ServerConfig config in Configs)
			{
				if (config.ConfigId == ConfigId)
				{
					return config;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		using FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
		}
		else
		{
			try
			{
				using StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);
				JsonElement val = JsonDocument.Parse(streamReader.ReadToEnd(), default(JsonDocumentOptions)).RootElement;
				val = ((JsonElement)(ref val)).GetProperty("Server");
				ArrayEnumerator val2 = ((JsonElement)(ref val)).EnumerateArray();
				ArrayEnumerator enumerator = ((ArrayEnumerator)(ref val2)).GetEnumerator();
				try
				{
					while (((ArrayEnumerator)(ref enumerator)).MoveNext())
					{
						JsonElement current = ((ArrayEnumerator)(ref enumerator)).Current;
						val = ((JsonElement)(ref current)).GetProperty("ConfigId");
						int num = int.Parse(((JsonElement)(ref val)).GetString());
						if (num != 0)
						{
							ServerConfig obj = new ServerConfig
							{
								ConfigId = num
							};
							val = ((JsonElement)(ref current)).GetProperty("ChannelOnlyGM");
							obj.OnlyGM = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("EnableMissions");
							obj.Missions = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("AccessUFL");
							obj.AccessUFL = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("UserFileList");
							obj.UserFileList = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("ClientVersion");
							obj.ClientVersion = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("EnableGiftSystem");
							obj.GiftSystem = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("EnableClan");
							obj.EnableClan = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("EnableTicket");
							obj.EnableTicket = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("EnablePlaytime");
							obj.EnablePlaytime = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("EnableTags");
							obj.EnableTags = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("EnableBlood");
							obj.EnableBlood = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("ExitURL");
							obj.ExitURL = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("ShopURL");
							obj.ShopURL = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("OfficialSteam");
							obj.OfficialSteam = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("OfficialBanner");
							obj.OfficialBanner = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("OfficialBannerEnabled");
							obj.OfficialBannerEnabled = bool.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("OfficialAddress");
							obj.OfficialAddress = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("ChatAnnoucementColor");
							obj.ChatAnnounceColor = int.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("ChannelAnnoucementColor");
							obj.ChannelAnnounceColor = int.Parse(((JsonElement)(ref val)).GetString());
							val = ((JsonElement)(ref current)).GetProperty("ChatAnnountcement");
							obj.ChatAnnounce = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("ChannelAnnouncement");
							obj.ChannelAnnounce = ((JsonElement)(ref val)).GetString();
							val = ((JsonElement)(ref current)).GetProperty("Showroom");
							obj.Showroom = ComDiv.ParseEnum<ShowroomView>(((JsonElement)(ref val)).GetString());
							ServerConfig item = obj;
							Configs.Add(item);
							continue;
						}
						CLogger.Print($"Invalid Config Id: {num}", LoggerType.Warning);
						return;
					}
				}
				finally
				{
					((IDisposable)(ArrayEnumerator)(ref enumerator)).Dispose();
				}
				streamReader.Dispose();
				streamReader.Close();
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
