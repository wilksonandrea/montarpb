// Decompiled with JetBrains decompiler
// Type: Plugin.Core.ConfigLoader
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
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

  [SpecialName]
  public T1 get_inx() => ((Class0<T0, T1>) this).gparam_1;

  [DebuggerHidden]
  public ConfigLoader([In] T0 obj0, [In] T1 obj1)
  {
    // ISSUE: reference to a compiler-generated field
    ((Class0<T0, T1>) this).gparam_0 = obj0;
    // ISSUE: reference to a compiler-generated field
    ((Class0<T0, T1>) this).gparam_1 = obj1;
  }

  [DebuggerHidden]
  public virtual bool System\u002EObject\u002EEquals([In] object obj0)
  {
    // ISSUE: variable of a compiler-generated type
    Class0<T0, T1> class0 = obj0 as Class0<T0, T1>;
    if (this == class0)
      return true;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return class0 != null && EqualityComparer<T0>.Default.Equals(((Class0<T0, T1>) this).gparam_0, class0.gparam_0) && EqualityComparer<T1>.Default.Equals(((Class0<T0, T1>) this).gparam_1, class0.gparam_1);
  }
}
