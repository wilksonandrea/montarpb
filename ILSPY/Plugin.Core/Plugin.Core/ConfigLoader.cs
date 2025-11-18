using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Core.Enums;
using Plugin.Core.Settings;

namespace Plugin.Core;

public static class ConfigLoader
{
	public static string[] HOST;

	public static readonly int[] DEFAULT_PORT;

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
		HOST = new string[3] { "0.0.0.0", "0.0.0.0", "0.0.0.0" };
		DEFAULT_PORT = new int[3] { 39190, 39191, 40009 };
		smethod_1();
		smethod_0();
	}

	private static void smethod_0()
	{
		ConfigEngine configEngine = new ConfigEngine("Config/Settings.ini", FileAccess.Read);
		HOST = new string[2]
		{
			configEngine.ReadS("PrivateIp4Address", "127.0.0.1", "Server"),
			configEngine.ReadS("PublicIp4Address", "127.0.0.1", "Server")
		};
		DatabaseHost = configEngine.ReadS("Host", "localhost", "Database");
		DatabaseName = configEngine.ReadS("Name", "", "Database");
		DatabaseUsername = configEngine.ReadS("User", "root", "Database");
		DatabasePassword = configEngine.ReadS("Pass", "", "Database");
		DatabasePort = configEngine.ReadD("Port", 0, "Database");
		ConfigId = configEngine.ReadD("ConfigId", 1, "Server");
		BackLog = configEngine.ReadD("BackLog", 3, "Server");
		DebugMode = configEngine.ReadX("Debug", Defaultprop: false, "Server");
		IsTestMode = configEngine.ReadX("Test", Defaultprop: false, "Server");
		ShowMoreInfo = configEngine.ReadX("MoreInfo", Defaultprop: false, "Server");
		IsDebugPing = configEngine.ReadX("DebugPing", Defaultprop: false, "Server");
		AutoBan = configEngine.ReadX("AutoBan", Defaultprop: false, "Server");
		ICafeSystem = configEngine.ReadX("ICafeSystem", Defaultprop: true, "Server");
		InternetCafeId = configEngine.ReadD("InternetCafeId", 1, "Server");
		AutoAccount = configEngine.ReadX("AutoAccount", Defaultprop: false, "Essentials");
		TournamentRule = configEngine.ReadX("TournamentRule", Defaultprop: false, "Essentials");
		MinRankVote = configEngine.ReadD("MinRankVote", 0, "Internal");
		WinCashPerBattle = configEngine.ReadX("WinCashPerBattle", Defaultprop: true, "Internal");
		ShowCashReceiveWarn = configEngine.ReadX("ShowCashReceiveWarn", Defaultprop: true, "Internal");
		MaxExpReward = configEngine.ReadD("MaxExpReward", 1000, "Internal");
		MaxGoldReward = configEngine.ReadD("MaxGoldReward", 1000, "Internal");
		MaxCashReward = configEngine.ReadD("MaxCashReward", 1000, "Internal");
		MinCreateRank = configEngine.ReadD("MinCreateRank", 15, "Internal");
		MinCreateGold = configEngine.ReadD("MinCreateGold", 7500, "Internal");
		MaxClanPoints = configEngine.ReadT("MaxClanPoints", 0f, "Internal");
		MaxActiveClans = configEngine.ReadD("MaxActiveClans", 0, "Internal");
		MaxLatency = configEngine.ReadD("MaxLatency", 0, "Internal");
		MaxRepeatLatency = configEngine.ReadD("MaxRepeatLatency", 0, "Internal");
		BattleRewardId = configEngine.ReadD("BattleRewardId", 1, "Internal");
		GrenateDamageMultipler = configEngine.ReadD("GrenateDamageMultipler", 2, "Internal");
		UdpType = (UdpState)configEngine.ReadC("UdpType", 3, "Others");
		UdpVersion = configEngine.ReadS("UdpVersion", "1508.7", "Others");
		SendInfoToServ = configEngine.ReadX("SendInfoToServ", Defaultprop: true, "Others");
		SendPingSync = configEngine.ReadX("SendPingSync", Defaultprop: true, "Others");
		EnableLog = configEngine.ReadX("EnableLog", Defaultprop: false, "Others");
		SendFailMsg = configEngine.ReadX("SendFailMsg", Defaultprop: true, "Others");
		UseHitMarker = configEngine.ReadX("UseHitMarker", Defaultprop: false, "Others");
		UseMaxAmmoInDrop = configEngine.ReadX("UseMaxAmmoInDrop", Defaultprop: false, "Others");
		MaxDropWpnCount = configEngine.ReadUH("MaxDropWpnCount", 0, "Others");
		AntiScript = configEngine.ReadX("AntiScript", Defaultprop: true, "Others");
		GameLocales = new List<ClientLocale>();
		National = (NationsEnum)Enum.Parse(typeof(NationsEnum), configEngine.ReadS("National", "Global", "Essentials"));
		string[] array = configEngine.ReadS("Region", "None", "Essentials").Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			Enum.TryParse<ClientLocale>(array[i], out var result);
			GameLocales.Add(result);
		}
		MinUserSize = 4;
		MaxUserSize = 32;
		MinPassSize = 4;
		MaxPassSize = 32;
		MinNickSize = 4;
		MaxNickSize = 16;
		PingUpdateTimeSeconds = 7;
		PlayersServerUpdateTimeSeconds = 7;
		UpdateIntervalPlayersServer = 2;
		EmptyRoomRemovalInterval = 2;
		ConsoleTitleUpdateTimeSeconds = 3;
		IntervalEnterRoomAfterKickSeconds = 30;
		MaxBuyItemDays = 365;
		MaxBuyItemUnits = 100000;
		PlantDuration = 5.5f;
		DefuseDuration = 7.1f;
	}

	private static void smethod_1()
	{
		ConfigEngine configEngine = new ConfigEngine("Config/Timeline.ini", FileAccess.Read);
		CustomYear = configEngine.ReadX("CustomYear", Defaultprop: false, "Addons");
		BackYear = configEngine.ReadD("BackYear", 10, "Runtime");
	}
}
