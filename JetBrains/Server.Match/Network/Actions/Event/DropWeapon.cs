// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.DropWeapon
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class DropWeapon
{
  public static void WriteInfo(SyncServerPacket S, AnimationInfo Info) => S.WriteH(Info.Animation);

  public static List<CharaFireNHitDataInfo> ReadInfo(
    ActionModel Action,
    SyncClientPacket C,
    bool GenLog,
    [In] bool obj3)
  {
    List<CharaFireNHitDataInfo> fireNhitDataInfoList = new List<CharaFireNHitDataInfo>();
    int num = (int) C.ReadC();
    for (int index = 0; index < num; ++index)
    {
      GrenadeHitInfo grenadeHitInfo = new GrenadeHitInfo();
      ((CharaFireNHitDataInfo) grenadeHitInfo).WeaponId = C.ReadD();
      ((CharaFireNHitDataInfo) grenadeHitInfo).Accessory = C.ReadC();
      ((CharaFireNHitDataInfo) grenadeHitInfo).Extensions = C.ReadC();
      ((CharaFireNHitDataInfo) grenadeHitInfo).HitInfo = C.ReadUD();
      ((CharaFireNHitDataInfo) grenadeHitInfo).Unk = C.ReadH();
      ((CharaFireNHitDataInfo) grenadeHitInfo).X = C.ReadUH();
      ((CharaFireNHitDataInfo) grenadeHitInfo).Y = C.ReadUH();
      ((CharaFireNHitDataInfo) grenadeHitInfo).Z = C.ReadUH();
      CharaFireNHitDataInfo fireNhitDataInfo = (CharaFireNHitDataInfo) grenadeHitInfo;
      if (!obj3)
        fireNhitDataInfo.WeaponClass = (ClassType) ComDiv.GetIdStatics(fireNhitDataInfo.WeaponId, 2);
      if (GenLog)
        CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {fireNhitDataInfo.WeaponId}; X: {fireNhitDataInfo.X} Y: {fireNhitDataInfo.Y} Z: {fireNhitDataInfo.Z}", LoggerType.Warning, (Exception) null);
      fireNhitDataInfoList.Add(fireNhitDataInfo);
    }
    return fireNhitDataInfoList;
  }
}
