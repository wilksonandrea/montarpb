using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;
using Plugin.Core.Settings;

namespace Plugin.Core
{
	// Token: 0x02000003 RID: 3
	public static class ConfigLoader
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00008110 File Offset: 0x00006310
		static ConfigLoader()
		{
			ConfigLoader.smethod_1();
			ConfigLoader.smethod_0();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00008160 File Offset: 0x00006360
		private static void smethod_0()
		{
			ConfigEngine configEngine = new ConfigEngine("Config/Settings.ini", FileAccess.Read);
			ConfigLoader.HOST = new string[]
			{
				configEngine.ReadS("PrivateIp4Address", "127.0.0.1", "Server"),
				configEngine.ReadS("PublicIp4Address", "127.0.0.1", "Server")
			};
			ConfigLoader.DatabaseHost = configEngine.ReadS("Host", "localhost", "Database");
			ConfigLoader.DatabaseName = configEngine.ReadS("Name", "", "Database");
			ConfigLoader.DatabaseUsername = configEngine.ReadS("User", "root", "Database");
			ConfigLoader.DatabasePassword = configEngine.ReadS("Pass", "", "Database");
			ConfigLoader.DatabasePort = configEngine.ReadD("Port", 0, "Database");
			ConfigLoader.ConfigId = configEngine.ReadD("ConfigId", 1, "Server");
			ConfigLoader.BackLog = configEngine.ReadD("BackLog", 3, "Server");
			ConfigLoader.DebugMode = configEngine.ReadX("Debug", false, "Server");
			ConfigLoader.IsTestMode = configEngine.ReadX("Test", false, "Server");
			ConfigLoader.ShowMoreInfo = configEngine.ReadX("MoreInfo", false, "Server");
			ConfigLoader.IsDebugPing = configEngine.ReadX("DebugPing", false, "Server");
			ConfigLoader.AutoBan = configEngine.ReadX("AutoBan", false, "Server");
			ConfigLoader.ICafeSystem = configEngine.ReadX("ICafeSystem", true, "Server");
			ConfigLoader.InternetCafeId = configEngine.ReadD("InternetCafeId", 1, "Server");
			ConfigLoader.AutoAccount = configEngine.ReadX("AutoAccount", false, "Essentials");
			ConfigLoader.TournamentRule = configEngine.ReadX("TournamentRule", false, "Essentials");
			ConfigLoader.MinRankVote = configEngine.ReadD("MinRankVote", 0, "Internal");
			ConfigLoader.WinCashPerBattle = configEngine.ReadX("WinCashPerBattle", true, "Internal");
			ConfigLoader.ShowCashReceiveWarn = configEngine.ReadX("ShowCashReceiveWarn", true, "Internal");
			ConfigLoader.MaxExpReward = configEngine.ReadD("MaxExpReward", 1000, "Internal");
			ConfigLoader.MaxGoldReward = configEngine.ReadD("MaxGoldReward", 1000, "Internal");
			ConfigLoader.MaxCashReward = configEngine.ReadD("MaxCashReward", 1000, "Internal");
			ConfigLoader.MinCreateRank = configEngine.ReadD("MinCreateRank", 15, "Internal");
			ConfigLoader.MinCreateGold = configEngine.ReadD("MinCreateGold", 7500, "Internal");
			ConfigLoader.MaxClanPoints = configEngine.ReadT("MaxClanPoints", 0f, "Internal");
			ConfigLoader.MaxActiveClans = configEngine.ReadD("MaxActiveClans", 0, "Internal");
			ConfigLoader.MaxLatency = configEngine.ReadD("MaxLatency", 0, "Internal");
			ConfigLoader.MaxRepeatLatency = configEngine.ReadD("MaxRepeatLatency", 0, "Internal");
			ConfigLoader.BattleRewardId = configEngine.ReadD("BattleRewardId", 1, "Internal");
			ConfigLoader.GrenateDamageMultipler = configEngine.ReadD("GrenateDamageMultipler", 2, "Internal");
			ConfigLoader.UdpType = (UdpState)configEngine.ReadC("UdpType", 3, "Others");
			ConfigLoader.UdpVersion = configEngine.ReadS("UdpVersion", "1508.7", "Others");
			ConfigLoader.SendInfoToServ = configEngine.ReadX("SendInfoToServ", true, "Others");
			ConfigLoader.SendPingSync = configEngine.ReadX("SendPingSync", true, "Others");
			ConfigLoader.EnableLog = configEngine.ReadX("EnableLog", false, "Others");
			ConfigLoader.SendFailMsg = configEngine.ReadX("SendFailMsg", true, "Others");
			ConfigLoader.UseHitMarker = configEngine.ReadX("UseHitMarker", false, "Others");
			ConfigLoader.UseMaxAmmoInDrop = configEngine.ReadX("UseMaxAmmoInDrop", false, "Others");
			ConfigLoader.MaxDropWpnCount = configEngine.ReadUH("MaxDropWpnCount", 0, "Others");
			ConfigLoader.AntiScript = configEngine.ReadX("AntiScript", true, "Others");
			ConfigLoader.GameLocales = new List<ClientLocale>();
			ConfigLoader.National = (NationsEnum)Enum.Parse(typeof(NationsEnum), configEngine.ReadS("National", "Global", "Essentials"));
			string[] array = configEngine.ReadS("Region", "None", "Essentials").Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				ClientLocale clientLocale;
				Enum.TryParse<ClientLocale>(array[i], out clientLocale);
				ConfigLoader.GameLocales.Add(clientLocale);
			}
			ConfigLoader.MinUserSize = 4;
			ConfigLoader.MaxUserSize = 32;
			ConfigLoader.MinPassSize = 4;
			ConfigLoader.MaxPassSize = 32;
			ConfigLoader.MinNickSize = 4;
			ConfigLoader.MaxNickSize = 16;
			ConfigLoader.PingUpdateTimeSeconds = 7;
			ConfigLoader.PlayersServerUpdateTimeSeconds = 7;
			ConfigLoader.UpdateIntervalPlayersServer = 2;
			ConfigLoader.EmptyRoomRemovalInterval = 2;
			ConfigLoader.ConsoleTitleUpdateTimeSeconds = 3;
			ConfigLoader.IntervalEnterRoomAfterKickSeconds = 30;
			ConfigLoader.MaxBuyItemDays = 365;
			ConfigLoader.MaxBuyItemUnits = 100000;
			ConfigLoader.PlantDuration = 5.5f;
			ConfigLoader.DefuseDuration = 7.1f;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020D1 File Offset: 0x000002D1
		private static void smethod_1()
		{
			ConfigEngine configEngine = new ConfigEngine("Config/Timeline.ini", FileAccess.Read);
			ConfigLoader.CustomYear = configEngine.ReadX("CustomYear", false, "Addons");
			ConfigLoader.BackYear = configEngine.ReadD("BackYear", 10, "Runtime");
		}

		// Token: 0x04000003 RID: 3
		public static string[] HOST = new string[] { "0.0.0.0", "0.0.0.0", "0.0.0.0" };

		// Token: 0x04000004 RID: 4
		public static readonly int[] DEFAULT_PORT = new int[] { 39190, 39191, 40009 };

		// Token: 0x04000005 RID: 5
		public static string DatabaseName;

		// Token: 0x04000006 RID: 6
		public static string DatabaseHost;

		// Token: 0x04000007 RID: 7
		public static string DatabaseUsername;

		// Token: 0x04000008 RID: 8
		public static string DatabasePassword;

		// Token: 0x04000009 RID: 9
		public static string UdpVersion;

		// Token: 0x0400000A RID: 10
		public static bool IsTestMode;

		// Token: 0x0400000B RID: 11
		public static bool ShowMoreInfo;

		// Token: 0x0400000C RID: 12
		public static bool AutoAccount;

		// Token: 0x0400000D RID: 13
		public static bool DebugMode;

		// Token: 0x0400000E RID: 14
		public static bool WinCashPerBattle;

		// Token: 0x0400000F RID: 15
		public static bool ShowCashReceiveWarn;

		// Token: 0x04000010 RID: 16
		public static bool AutoBan;

		// Token: 0x04000011 RID: 17
		public static bool SendInfoToServ;

		// Token: 0x04000012 RID: 18
		public static bool SendFailMsg;

		// Token: 0x04000013 RID: 19
		public static bool EnableLog;

		// Token: 0x04000014 RID: 20
		public static bool UseMaxAmmoInDrop;

		// Token: 0x04000015 RID: 21
		public static bool UseHitMarker;

		// Token: 0x04000016 RID: 22
		public static bool ICafeSystem;

		// Token: 0x04000017 RID: 23
		public static bool IsDebugPing;

		// Token: 0x04000018 RID: 24
		public static bool CustomYear;

		// Token: 0x04000019 RID: 25
		public static bool AntiScript;

		// Token: 0x0400001A RID: 26
		public static bool SendPingSync;

		// Token: 0x0400001B RID: 27
		public static bool TournamentRule;

		// Token: 0x0400001C RID: 28
		public static int DatabasePort;

		// Token: 0x0400001D RID: 29
		public static int ConfigId;

		// Token: 0x0400001E RID: 30
		public static int MaxNickSize;

		// Token: 0x0400001F RID: 31
		public static int MinNickSize;

		// Token: 0x04000020 RID: 32
		public static int MaxUserSize;

		// Token: 0x04000021 RID: 33
		public static int MinUserSize;

		// Token: 0x04000022 RID: 34
		public static int MaxPassSize;

		// Token: 0x04000023 RID: 35
		public static int MinPassSize;

		// Token: 0x04000024 RID: 36
		public static int BackLog;

		// Token: 0x04000025 RID: 37
		public static int MaxLatency;

		// Token: 0x04000026 RID: 38
		public static int MaxRepeatLatency;

		// Token: 0x04000027 RID: 39
		public static int MaxActiveClans;

		// Token: 0x04000028 RID: 40
		public static int MinRankVote;

		// Token: 0x04000029 RID: 41
		public static int MaxExpReward;

		// Token: 0x0400002A RID: 42
		public static int MaxGoldReward;

		// Token: 0x0400002B RID: 43
		public static int MaxCashReward;

		// Token: 0x0400002C RID: 44
		public static int MinCreateGold;

		// Token: 0x0400002D RID: 45
		public static int MinCreateRank;

		// Token: 0x0400002E RID: 46
		public static int InternetCafeId;

		// Token: 0x0400002F RID: 47
		public static int BackYear;

		// Token: 0x04000030 RID: 48
		public static int PingUpdateTimeSeconds;

		// Token: 0x04000031 RID: 49
		public static int PlayersServerUpdateTimeSeconds;

		// Token: 0x04000032 RID: 50
		public static int UpdateIntervalPlayersServer;

		// Token: 0x04000033 RID: 51
		public static int EmptyRoomRemovalInterval;

		// Token: 0x04000034 RID: 52
		public static int ConsoleTitleUpdateTimeSeconds;

		// Token: 0x04000035 RID: 53
		public static int IntervalEnterRoomAfterKickSeconds;

		// Token: 0x04000036 RID: 54
		public static int MaxBuyItemDays;

		// Token: 0x04000037 RID: 55
		public static int MaxBuyItemUnits;

		// Token: 0x04000038 RID: 56
		public static int BattleRewardId;

		// Token: 0x04000039 RID: 57
		public static int GrenateDamageMultipler;

		// Token: 0x0400003A RID: 58
		public static float MaxClanPoints;

		// Token: 0x0400003B RID: 59
		public static float PlantDuration;

		// Token: 0x0400003C RID: 60
		public static float DefuseDuration;

		// Token: 0x0400003D RID: 61
		public static ushort MaxDropWpnCount;

		// Token: 0x0400003E RID: 62
		public static UdpState UdpType;

		// Token: 0x0400003F RID: 63
		public static NationsEnum National;

		// Token: 0x04000040 RID: 64
		public static List<ClientLocale> GameLocales;
	}
}
