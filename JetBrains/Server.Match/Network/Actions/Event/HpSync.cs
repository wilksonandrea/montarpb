// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.HpSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class HpSync
{
  public static void WriteInfo([In] SyncServerPacket obj0, List<GrenadeHitInfo> Info)
  {
    obj0.WriteC((byte) Info.Count);
    foreach (GrenadeHitInfo grenadeHitInfo in Info)
    {
      obj0.WriteD(grenadeHitInfo.WeaponId);
      obj0.WriteC(grenadeHitInfo.Accessory);
      obj0.WriteC(grenadeHitInfo.Extensions);
      obj0.WriteH(grenadeHitInfo.BoomInfo);
      obj0.WriteH(grenadeHitInfo.ObjectId);
      obj0.WriteD(grenadeHitInfo.HitInfo);
      obj0.WriteHV(grenadeHitInfo.PlayerPos);
      obj0.WriteHV(grenadeHitInfo.FirePos);
      obj0.WriteHV(grenadeHitInfo.HitPos);
      obj0.WriteH(grenadeHitInfo.GrenadesCount);
      obj0.WriteC((byte) grenadeHitInfo.DeathType);
    }
  }

  public static List<HitDataInfo> ReadInfo(
    [In] ActionModel obj0,
    SyncClientPacket C,
    bool GenLog,
    bool OnlyBytes = false)
  {
    List<HitDataInfo> hitDataInfoList = new List<HitDataInfo>();
    int num1 = (int) C.ReadC();
    for (int index1 = 0; index1 < num1; ++index1)
    {
      UseObjectInfo useObjectInfo = new UseObjectInfo();
      ((HitDataInfo) useObjectInfo).StartBullet = C.ReadTV();
      ((HitDataInfo) useObjectInfo).EndBullet = C.ReadTV();
      ((HitDataInfo) useObjectInfo).BulletPos = C.ReadTV();
      ((HitDataInfo) useObjectInfo).BoomInfo = C.ReadUH();
      ((HitDataInfo) useObjectInfo).ObjectId = C.ReadUH();
      ((HitDataInfo) useObjectInfo).HitIndex = C.ReadUD();
      ((HitDataInfo) useObjectInfo).WeaponId = C.ReadD();
      ((HitDataInfo) useObjectInfo).Accessory = C.ReadC();
      ((HitDataInfo) useObjectInfo).Extensions = C.ReadC();
      HitDataInfo hitDataInfo = (HitDataInfo) useObjectInfo;
      if (!OnlyBytes)
      {
        hitDataInfo.HitEnum = (HitType) AllUtils.GetHitHelmet(hitDataInfo.HitIndex);
        if (hitDataInfo.BoomInfo > (ushort) 0)
        {
          hitDataInfo.BoomPlayers = new List<int>();
          for (int index2 = 0; index2 < 18; ++index2)
          {
            int num2 = 1 << index2;
            if (((int) hitDataInfo.BoomInfo & num2) == num2)
              hitDataInfo.BoomPlayers.Add(index2);
          }
        }
        hitDataInfo.WeaponClass = (ClassType) ComDiv.GetIdStatics(hitDataInfo.WeaponId, 2);
      }
      if (GenLog)
      {
        CLogger.Print($"PVP Slot: {obj0.Slot}; Weapon Id: {hitDataInfo.WeaponId}; Ext: {hitDataInfo.Extensions}; Acc: {hitDataInfo.Accessory}", LoggerType.Warning, (Exception) null);
        CLogger.Print($"PVP Slot: {obj0.Slot}; Hit Data: {hitDataInfo.HitIndex} [Start]: X: {hitDataInfo.StartBullet.X}; Y: {hitDataInfo.StartBullet.Y}; Z: {hitDataInfo.StartBullet.Z}", LoggerType.Warning, (Exception) null);
        CLogger.Print($"PVP Slot: {obj0.Slot}; Hit Data: {hitDataInfo.HitIndex} [Ended]: X: {hitDataInfo.EndBullet.X}; Y: {hitDataInfo.EndBullet.Y}; Z: {hitDataInfo.EndBullet.Z}", LoggerType.Warning, (Exception) null);
      }
      hitDataInfoList.Add(hitDataInfo);
    }
    return hitDataInfoList;
  }
}
