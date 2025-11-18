// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.FireData
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

public class FireData
{
  public static void WriteInfo([In] SyncServerPacket obj0, List<CharaFireNHitDataInfo> Info)
  {
    obj0.WriteC((byte) Info.Count);
    foreach (CharaFireNHitDataInfo fireNhitDataInfo in Info)
    {
      obj0.WriteD(fireNhitDataInfo.WeaponId);
      obj0.WriteC(fireNhitDataInfo.Accessory);
      obj0.WriteC(fireNhitDataInfo.Extensions);
      obj0.WriteD(fireNhitDataInfo.HitInfo);
      obj0.WriteH(fireNhitDataInfo.Unk);
      obj0.WriteH(fireNhitDataInfo.X);
      obj0.WriteH(fireNhitDataInfo.Y);
      obj0.WriteH(fireNhitDataInfo.Z);
    }
  }

  public static DropWeaponInfo ReadInfo([In] ActionModel obj0, SyncClientPacket C, bool GenLog)
  {
    HitDataInfo hitDataInfo = new HitDataInfo();
    ((DropWeaponInfo) hitDataInfo).AmmoPrin = C.ReadUH();
    ((DropWeaponInfo) hitDataInfo).AmmoDual = C.ReadUH();
    ((DropWeaponInfo) hitDataInfo).AmmoTotal = C.ReadUH();
    ((DropWeaponInfo) hitDataInfo).Unk1 = C.ReadUH();
    ((DropWeaponInfo) hitDataInfo).Unk2 = C.ReadUH();
    ((DropWeaponInfo) hitDataInfo).Unk3 = C.ReadUH();
    ((DropWeaponInfo) hitDataInfo).Flags = C.ReadC();
    ((DropWeaponInfo) hitDataInfo).WeaponId = C.ReadD();
    ((DropWeaponInfo) hitDataInfo).Accessory = C.ReadC();
    ((DropWeaponInfo) hitDataInfo).Extensions = C.ReadC();
    DropWeaponInfo dropWeaponInfo = (DropWeaponInfo) hitDataInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot : {obj0.Slot}; Weapon Id: {dropWeaponInfo.WeaponId}; Ext: {dropWeaponInfo.Extensions}; Acc: {dropWeaponInfo.Accessory}; Ammo Prin: {dropWeaponInfo.AmmoPrin}; Ammo Dual: {dropWeaponInfo.AmmoDual}; Ammo Total: {dropWeaponInfo.AmmoTotal}", LoggerType.Warning, (Exception) null);
    return dropWeaponInfo;
  }
}
