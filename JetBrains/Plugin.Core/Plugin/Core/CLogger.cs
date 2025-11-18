// Decompiled with JetBrains decompiler
// Type: Plugin.Core.CLogger
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Plugin.Core;

public static class CLogger
{
  private static readonly string string_0;
  private static readonly string string_1;
  private static readonly string string_2;
  private static readonly string string_3;
  private static readonly string string_4;
  private static readonly string string_5;
  private static readonly string string_6;
  private static readonly object object_0;

  [DebuggerHidden]
  public virtual int System\u002EObject\u002EGetHashCode()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (EqualityComparer<T0>.Default.GetHashCode(((Class0<T0, T1>) this).gparam_0) - 1959725626) * -1521134295 + EqualityComparer<T1>.Default.GetHashCode(((Class0<T0, T1>) this).gparam_1);
  }

  [DebuggerHidden]
  public virtual string System\u002EObject\u002EToString()
  {
    object[] objArray = new object[2];
    // ISSUE: reference to a compiler-generated field
    T0 gparam0 = ((Class0<T0, T1>) this).gparam_0;
    ref T0 local1 = ref gparam0;
    objArray[0] = (object) ((object) local1 != null ? local1.ToString() : (string) null);
    // ISSUE: reference to a compiler-generated field
    T1 gparam1 = ((Class0<T0, T1>) this).gparam_1;
    ref T1 local2 = ref gparam1;
    objArray[1] = (object) ((object) local2 != null ? local2.ToString() : (string) null);
    return string.Format((IFormatProvider) null, "{{ item = {0}, inx = {1} }}", objArray);
  }

  static CLogger()
  {
    ConfigLoader.HOST = new string[3]
    {
      "0.0.0.0",
      "0.0.0.0",
      "0.0.0.0"
    };
    ConfigLoader.DEFAULT_PORT = new int[3]
    {
      39190,
      39191,
      40009
    };
    CLogger.smethod_1();
    CLogger.smethod_0();
  }

  private static void smethod_0()
  {
    ConfigEngine configEngine = new ConfigEngine("Config/Settings.ini", FileAccess.Read);
    ConfigLoader.HOST = new string[2]
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
    ConfigLoader.MaxClanPoints = configEngine.ReadT("MaxClanPoints", 0.0f, "Internal");
    ConfigLoader.MaxActiveClans = configEngine.ReadD("MaxActiveClans", 0, "Internal");
    ConfigLoader.MaxLatency = configEngine.ReadD("MaxLatency", 0, "Internal");
    ConfigLoader.MaxRepeatLatency = configEngine.ReadD("MaxRepeatLatency", 0, "Internal");
    ConfigLoader.BattleRewardId = configEngine.ReadD("BattleRewardId", 1, "Internal");
    ConfigLoader.GrenateDamageMultipler = configEngine.ReadD("GrenateDamageMultipler", 2, "Internal");
    ConfigLoader.UdpType = (UdpState) configEngine.ReadC("UdpType", (byte) 3, "Others");
    ConfigLoader.UdpVersion = configEngine.ReadS("UdpVersion", "1508.7", "Others");
    ConfigLoader.SendInfoToServ = configEngine.ReadX("SendInfoToServ", true, "Others");
    ConfigLoader.SendPingSync = configEngine.ReadX("SendPingSync", true, "Others");
    ConfigLoader.EnableLog = configEngine.ReadX("EnableLog", false, "Others");
    ConfigLoader.SendFailMsg = configEngine.ReadX("SendFailMsg", true, "Others");
    ConfigLoader.UseHitMarker = configEngine.ReadX("UseHitMarker", false, "Others");
    ConfigLoader.UseMaxAmmoInDrop = configEngine.ReadX("UseMaxAmmoInDrop", false, "Others");
    ConfigLoader.MaxDropWpnCount = configEngine.ReadUH("MaxDropWpnCount", (ushort) 0, "Others");
    ConfigLoader.AntiScript = configEngine.ReadX("AntiScript", true, "Others");
    ConfigLoader.GameLocales = new List<ClientLocale>();
    ConfigLoader.National = (NationsEnum) Enum.Parse(typeof (NationsEnum), configEngine.ReadS("National", "Global", "Essentials"));
    string str1 = configEngine.ReadS("Region", "None", "Essentials");
    char[] chArray = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray))
    {
      ClientLocale result;
      Enum.TryParse<ClientLocale>(str2, out result);
      ConfigLoader.GameLocales.Add(result);
    }
    ConfigLoader.MinUserSize = 4;
    ConfigLoader.MaxUserSize = 32 /*0x20*/;
    ConfigLoader.MinPassSize = 4;
    ConfigLoader.MaxPassSize = 32 /*0x20*/;
    ConfigLoader.MinNickSize = 4;
    ConfigLoader.MaxNickSize = 16 /*0x10*/;
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
