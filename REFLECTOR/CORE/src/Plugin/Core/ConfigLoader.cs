namespace Plugin.Core
{
    using Plugin.Core.Enums;
    using Plugin.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class ConfigLoader
    {
        public static string[] HOST = new string[] { "0.0.0.0", "0.0.0.0", "0.0.0.0" };
        public static readonly int[] DEFAULT_PORT = new int[] { 0x9916, 0x9917, 0x9c49 };
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
            smethod_1();
            smethod_0();
        }

        private static void smethod_0()
        {
            ConfigEngine engine = new ConfigEngine("Config/Settings.ini", FileAccess.Read);
            HOST = new string[] { engine.ReadS("PrivateIp4Address", "127.0.0.1", "Server"), engine.ReadS("PublicIp4Address", "127.0.0.1", "Server") };
            DatabaseHost = engine.ReadS("Host", "localhost", "Database");
            DatabaseName = engine.ReadS("Name", "", "Database");
            DatabaseUsername = engine.ReadS("User", "root", "Database");
            DatabasePassword = engine.ReadS("Pass", "", "Database");
            DatabasePort = engine.ReadD("Port", 0, "Database");
            ConfigId = engine.ReadD("ConfigId", 1, "Server");
            BackLog = engine.ReadD("BackLog", 3, "Server");
            DebugMode = engine.ReadX("Debug", false, "Server");
            IsTestMode = engine.ReadX("Test", false, "Server");
            ShowMoreInfo = engine.ReadX("MoreInfo", false, "Server");
            IsDebugPing = engine.ReadX("DebugPing", false, "Server");
            AutoBan = engine.ReadX("AutoBan", false, "Server");
            ICafeSystem = engine.ReadX("ICafeSystem", true, "Server");
            InternetCafeId = engine.ReadD("InternetCafeId", 1, "Server");
            AutoAccount = engine.ReadX("AutoAccount", false, "Essentials");
            TournamentRule = engine.ReadX("TournamentRule", false, "Essentials");
            MinRankVote = engine.ReadD("MinRankVote", 0, "Internal");
            WinCashPerBattle = engine.ReadX("WinCashPerBattle", true, "Internal");
            ShowCashReceiveWarn = engine.ReadX("ShowCashReceiveWarn", true, "Internal");
            MaxExpReward = engine.ReadD("MaxExpReward", 0x3e8, "Internal");
            MaxGoldReward = engine.ReadD("MaxGoldReward", 0x3e8, "Internal");
            MaxCashReward = engine.ReadD("MaxCashReward", 0x3e8, "Internal");
            MinCreateRank = engine.ReadD("MinCreateRank", 15, "Internal");
            MinCreateGold = engine.ReadD("MinCreateGold", 0x1d4c, "Internal");
            MaxClanPoints = engine.ReadT("MaxClanPoints", 0f, "Internal");
            MaxActiveClans = engine.ReadD("MaxActiveClans", 0, "Internal");
            MaxLatency = engine.ReadD("MaxLatency", 0, "Internal");
            MaxRepeatLatency = engine.ReadD("MaxRepeatLatency", 0, "Internal");
            BattleRewardId = engine.ReadD("BattleRewardId", 1, "Internal");
            GrenateDamageMultipler = engine.ReadD("GrenateDamageMultipler", 2, "Internal");
            UdpType = (UdpState) engine.ReadC("UdpType", 3, "Others");
            UdpVersion = engine.ReadS("UdpVersion", "1508.7", "Others");
            SendInfoToServ = engine.ReadX("SendInfoToServ", true, "Others");
            SendPingSync = engine.ReadX("SendPingSync", true, "Others");
            EnableLog = engine.ReadX("EnableLog", false, "Others");
            SendFailMsg = engine.ReadX("SendFailMsg", true, "Others");
            UseHitMarker = engine.ReadX("UseHitMarker", false, "Others");
            UseMaxAmmoInDrop = engine.ReadX("UseMaxAmmoInDrop", false, "Others");
            MaxDropWpnCount = engine.ReadUH("MaxDropWpnCount", 0, "Others");
            AntiScript = engine.ReadX("AntiScript", true, "Others");
            GameLocales = new List<ClientLocale>();
            National = (NationsEnum) Enum.Parse(typeof(NationsEnum), engine.ReadS("National", "Global", "Essentials"));
            char[] separator = new char[] { ',' };
            string[] strArray = engine.ReadS("Region", "None", "Essentials").Split(separator);
            for (int i = 0; i < strArray.Length; i++)
            {
                ClientLocale locale;
                Enum.TryParse<ClientLocale>(strArray[i], out locale);
                GameLocales.Add(locale);
            }
            MinUserSize = 4;
            MaxUserSize = 0x20;
            MinPassSize = 4;
            MaxPassSize = 0x20;
            MinNickSize = 4;
            MaxNickSize = 0x10;
            PingUpdateTimeSeconds = 7;
            PlayersServerUpdateTimeSeconds = 7;
            UpdateIntervalPlayersServer = 2;
            EmptyRoomRemovalInterval = 2;
            ConsoleTitleUpdateTimeSeconds = 3;
            IntervalEnterRoomAfterKickSeconds = 30;
            MaxBuyItemDays = 0x16d;
            MaxBuyItemUnits = 0x186a0;
            PlantDuration = 5.5f;
            DefuseDuration = 7.1f;
        }

        private static void smethod_1()
        {
            ConfigEngine engine1 = new ConfigEngine("Config/Timeline.ini", FileAccess.Read);
            CustomYear = engine1.ReadX("CustomYear", false, "Addons");
            BackYear = engine1.ReadD("BackYear", 10, "Runtime");
        }
    }
}

