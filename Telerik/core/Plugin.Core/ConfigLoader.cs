using Plugin.Core.Enums;
using Plugin.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;

namespace Plugin.Core
{
	public static class ConfigLoader
	{
		public static string[] HOST;

		public readonly static int[] DEFAULT_PORT;

		public static string DatabaseName;

		public static string DatabaseHost;

		public static string DatabaseUsername;

		public static string DatabasePassword;

		public static string UdpVersion;

		public static bool IsTestMode;

		public static bool ShowMoreInfo;

		public static bool AutoAccount;

		public static bool DebugMode;

		public static bool WinCashPerBattle;

		public static bool ShowCashReceiveWarn;

		public static bool AutoBan;

		public static bool SendInfoToServ;

		public static bool SendFailMsg;

		public static bool EnableLog;

		public static bool UseMaxAmmoInDrop;

		public static bool UseHitMarker;

		public static bool ICafeSystem;

		public static bool IsDebugPing;

		public static bool CustomYear;

		public static bool AntiScript;

		public static bool SendPingSync;

		public static bool TournamentRule;

		public static int DatabasePort;

		public static int ConfigId;

		public static int MaxNickSize;

		public static int MinNickSize;

		public static int MaxUserSize;

		public static int MinUserSize;

		public static int MaxPassSize;

		public static int MinPassSize;

		public static int BackLog;

		public static int MaxLatency;

		public static int MaxRepeatLatency;

		public static int MaxActiveClans;

		public static int MinRankVote;

		public static int MaxExpReward;

		public static int MaxGoldReward;

		public static int MaxCashReward;

		public static int MinCreateGold;

		public static int MinCreateRank;

		public static int InternetCafeId;

		public static int BackYear;

		public static int PingUpdateTimeSeconds;

		public static int PlayersServerUpdateTimeSeconds;

		public static int UpdateIntervalPlayersServer;

		public static int EmptyRoomRemovalInterval;

		public static int ConsoleTitleUpdateTimeSeconds;

		public static int IntervalEnterRoomAfterKickSeconds;

		public static int MaxBuyItemDays;

		public static int MaxBuyItemUnits;

		public static int BattleRewardId;

		public static int GrenateDamageMultipler;

		public static float MaxClanPoints;

		public static float PlantDuration;

		public static float DefuseDuration;

		public static ushort MaxDropWpnCount;

		public static UdpState UdpType;

		public static NationsEnum National;

		public static List<ClientLocale> GameLocales;

		static ConfigLoader()
		{
			ConfigLoader.HOST = new string[] { "0.0.0.0", "0.0.0.0", "0.0.0.0" };
			ConfigLoader.DEFAULT_PORT = new int[] { 39190, 39191, 40009 };
			ConfigLoader.smethod_1();
			ConfigLoader.smethod_0();
		}

		private static void smethod_0()
		{
			ClientLocale clientLocale;
			ConfigEngine configEngine = new ConfigEngine("Config/Settings.ini", FileAccess.Read);
			ConfigLoader.HOST = new string[] { configEngine.ReadS("PrivateIp4Address", "127.0.0.1", "Server"), configEngine.ReadS("PublicIp4Address", "127.0.0.1", "Server") };
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
			string[] strArrays = configEngine.ReadS("Region", "None", "Essentials").Split(new char[] { ',' });
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				Enum.TryParse<ClientLocale>(strArrays[i], out clientLocale);
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

		private static void smethod_1()
		{
			ConfigEngine configEngine = new ConfigEngine("Config/Timeline.ini", FileAccess.Read);
			ConfigLoader.CustomYear = configEngine.ReadX("CustomYear", false, "Addons");
			ConfigLoader.BackYear = configEngine.ReadD("BackYear", 10, "Runtime");
		}
	}
}