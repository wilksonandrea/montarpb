// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.GrenadeSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class GrenadeSync
{
  public static void WriteInfo([In] SyncServerPacket obj0, StageControlInfo GenLog)
  {
    obj0.WriteB(GenLog.Unk);
  }

  public static DropedWeaponInfo ReadInfo([In] SyncClientPacket obj0, bool Info)
  {
    StageStaticInfo stageStaticInfo = new StageStaticInfo();
    ((DropedWeaponInfo) stageStaticInfo).WeaponPos = obj0.ReadUHV();
    ((DropedWeaponInfo) stageStaticInfo).Unk1 = obj0.ReadUH();
    ((DropedWeaponInfo) stageStaticInfo).Unk2 = obj0.ReadUH();
    ((DropedWeaponInfo) stageStaticInfo).Unk3 = obj0.ReadUH();
    ((DropedWeaponInfo) stageStaticInfo).Unk4 = obj0.ReadUH();
    ((DropedWeaponInfo) stageStaticInfo).WeaponFlag = obj0.ReadC();
    ((DropedWeaponInfo) stageStaticInfo).Unks = obj0.ReadB(16 /*0x10*/);
    DropedWeaponInfo dropedWeaponInfo = (DropedWeaponInfo) stageStaticInfo;
    if (Info)
      CLogger.Print($"Sub Head: DroppedWeapon; Weapon Flag: {dropedWeaponInfo.WeaponFlag}; X: {dropedWeaponInfo.WeaponPos.X}; Y: {dropedWeaponInfo.WeaponPos.Y}; Z: {dropedWeaponInfo.WeaponPos.Z}", LoggerType.Warning, (Exception) null);
    return dropedWeaponInfo;
  }
}
