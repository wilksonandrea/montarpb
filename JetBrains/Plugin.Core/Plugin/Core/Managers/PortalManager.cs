// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Managers.PortalManager
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Plugin.Core.Managers;

public static class PortalManager
{
  public static SortedList<string, PortalEvents> AllEvents;

  [CompilerGenerated]
  [SpecialName]
  public int get_StageOptions() => ((MapRule) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_StageOptions(int value) => ((MapRule) this).int_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Conditions() => ((MapRule) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_Conditions(int value) => ((MapRule) this).int_3 = value;

  public PortalManager() => ((MapRule) this).Name = "";

  public static void Load()
  {
    foreach (EventBoostModel eventBoostModel in EventBoostXML.Events)
    {
      if (eventBoostModel != null && ((EventPlaytimeModel) eventBoostModel).EventIsEnabled())
        PortalManager.AllEvents.Add($"Boost_{eventBoostModel.Id}", PortalEvents.BoostEvent);
    }
    foreach (EventRankUpModel eventRankUpModel in EventRankUpXML.Events)
    {
      if (eventRankUpModel != null && ((EventVisitModel) eventRankUpModel).EventIsEnabled())
        PortalManager.AllEvents.Add($"RankUp_{eventRankUpModel.Id}", PortalEvents.RankUpEvent);
    }
    foreach (EventLoginModel eventLoginModel in EventLoginXML.Events)
    {
      if (eventLoginModel != null && ((EventBoostModel) eventLoginModel).EventIsEnabled())
        PortalManager.AllEvents.Add($"Login_{eventLoginModel.Id}", PortalEvents.LoginEvent);
    }
    foreach (EventPlaytimeModel eventPlaytimeModel in EventPlaytimeXML.Events)
    {
      if (eventPlaytimeModel != null && ((EventQuestModel) eventPlaytimeModel).EventIsEnabled())
        PortalManager.AllEvents.Add($"Playtime_{eventPlaytimeModel.Id}", PortalEvents.PlaytimeEvent);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {PortalManager.AllEvents.Count} Listed Event Portal", LoggerType.Info, (Exception) null);
  }

  public static int GetInitialId(string value)
  {
    Match match = Regex.Match(value, "\\d+");
    int result;
    return match.Success && int.TryParse(match.Value, out result) ? result : -1;
  }

  public static byte[] InitEventData(
    PortalEvents value,
    [In] int obj1,
    [In] uint[] obj2,
    [In] string[] obj3,
    [In] byte[] obj4,
    [In] ushort obj5)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) value);
      syncServerPacket.WriteD(obj1);
      syncServerPacket.WriteC(obj4[0]);
      syncServerPacket.WriteD(obj2[0]);
      syncServerPacket.WriteD(obj2[1]);
      syncServerPacket.WriteD(0);
      syncServerPacket.WriteC(obj4[1]);
      ((BaseServerPacket) syncServerPacket).WriteU(obj3[0], 120);
      ((BaseServerPacket) syncServerPacket).WriteU(obj3[1], 200);
      syncServerPacket.WriteH(obj5);
      return syncServerPacket.ToArray();
    }
  }
}
