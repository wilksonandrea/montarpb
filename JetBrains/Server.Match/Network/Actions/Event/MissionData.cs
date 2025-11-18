// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.MissionData
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
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class MissionData
{
  public static void WriteInfo([In] SyncServerPacket obj0, List<HitDataInfo> Hits)
  {
    obj0.WriteC((byte) Hits.Count);
    foreach (HitDataInfo hit in Hits)
    {
      obj0.WriteTV(hit.StartBullet);
      obj0.WriteTV(hit.EndBullet);
      obj0.WriteTV(hit.BulletPos);
      obj0.WriteH(hit.BoomInfo);
      obj0.WriteH(hit.ObjectId);
      obj0.WriteD(hit.HitIndex);
      obj0.WriteD(hit.WeaponId);
      obj0.WriteC(hit.Accessory);
      obj0.WriteC(hit.Extensions);
    }
  }

  public static HPSyncInfo ReadInfo([In] ActionModel obj0, SyncClientPacket C, bool genLog)
  {
    AnimationInfo animationInfo = new AnimationInfo();
    ((HPSyncInfo) animationInfo).CharaLife = C.ReadUH();
    HPSyncInfo hpSyncInfo = (HPSyncInfo) animationInfo;
    if (genLog)
      CLogger.Print($"PVP Slot: {obj0.Slot}; is using Chara with HP ({hpSyncInfo.CharaLife})", LoggerType.Warning, (Exception) null);
    return hpSyncInfo;
  }
}
