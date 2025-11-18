// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.WeaponRecoil
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class WeaponRecoil
{
  public static void WriteInfo([In] SyncServerPacket obj0, List<SuicideInfo> Info)
  {
    obj0.WriteC((byte) Info.Count);
    foreach (SuicideInfo suicideInfo in Info)
    {
      obj0.WriteHV(suicideInfo.PlayerPos);
      obj0.WriteD(suicideInfo.WeaponId);
      obj0.WriteC(suicideInfo.Accessory);
      obj0.WriteC(suicideInfo.Extensions);
      obj0.WriteD(suicideInfo.HitInfo);
    }
  }

  public static List<UseObjectInfo> ReadInfo([In] ActionModel obj0, SyncClientPacket C, bool GenLog)
  {
    List<UseObjectInfo> useObjectInfoList = new List<UseObjectInfo>();
    int num = (int) C.ReadC();
    for (int index = 0; index < num; ++index)
    {
      AssistManager assistManager = new AssistManager();
      ((UseObjectInfo) assistManager).ObjectId = C.ReadUH();
      ((UseObjectInfo) assistManager).Use = C.ReadC();
      ((UseObjectInfo) assistManager).SpaceFlags = (CharaMoves) C.ReadC();
      UseObjectInfo useObjectInfo = (UseObjectInfo) assistManager;
      if (GenLog)
        CLogger.Print($"PVP Slot: {obj0.Slot}; Use Object: {useObjectInfo.Use}; Flag: {useObjectInfo.SpaceFlags}; ObjectId: {useObjectInfo.ObjectId}", LoggerType.Warning, (Exception) null);
      useObjectInfoList.Add(useObjectInfo);
    }
    return useObjectInfoList;
  }
}
