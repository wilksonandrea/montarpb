// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.WeaponSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class WeaponSync
{
  public static void WriteInfo(SyncServerPacket S, List<UseObjectInfo> Hits)
  {
    S.WriteC((byte) Hits.Count);
    foreach (UseObjectInfo hit in Hits)
    {
      S.WriteH(hit.ObjectId);
      S.WriteC(hit.Use);
      S.WriteC((byte) hit.SpaceFlags);
    }
  }

  public static WeaponRecoilInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    FireDataInfo fireDataInfo = new FireDataInfo();
    ((WeaponRecoilInfo) fireDataInfo).RecoilHorzAngle = C.ReadT();
    ((WeaponRecoilInfo) fireDataInfo).RecoilHorzMax = C.ReadT();
    ((WeaponRecoilInfo) fireDataInfo).RecoilVertAngle = C.ReadT();
    ((WeaponRecoilInfo) fireDataInfo).RecoilVertMax = C.ReadT();
    ((WeaponRecoilInfo) fireDataInfo).Deviation = C.ReadT();
    ((WeaponRecoilInfo) fireDataInfo).RecoilHorzCount = C.ReadC();
    ((WeaponRecoilInfo) fireDataInfo).WeaponId = C.ReadD();
    ((WeaponRecoilInfo) fireDataInfo).Accessory = C.ReadC();
    ((WeaponRecoilInfo) fireDataInfo).Extensions = C.ReadC();
    WeaponRecoilInfo weaponRecoilInfo = (WeaponRecoilInfo) fireDataInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; WeaponId: {weaponRecoilInfo.WeaponId}", LoggerType.Warning, (Exception) null);
    return weaponRecoilInfo;
  }
}
