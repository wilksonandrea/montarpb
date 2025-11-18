// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.ObjectAnim
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.SubHead;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class ObjectAnim
{
  public static void WriteInfo([In] SyncServerPacket obj0, DropedWeaponInfo GenLog)
  {
    obj0.WriteHV(GenLog.WeaponPos);
    obj0.WriteH(GenLog.Unk1);
    obj0.WriteH(GenLog.Unk2);
    obj0.WriteH(GenLog.Unk3);
    obj0.WriteH(GenLog.Unk4);
    obj0.WriteC(GenLog.WeaponFlag);
    obj0.WriteB(GenLog.Unks);
  }

  public static GrenadeInfo ReadInfo([In] SyncClientPacket obj0, bool Info, [In] bool obj2)
  {
    HPSyncInfo hpSyncInfo = new HPSyncInfo();
    ((GrenadeInfo) hpSyncInfo).Unk1 = obj0.ReadC();
    ((GrenadeInfo) hpSyncInfo).Unk2 = obj0.ReadC();
    ((GrenadeInfo) hpSyncInfo).Unk3 = obj0.ReadC();
    ((GrenadeInfo) hpSyncInfo).Unk4 = obj0.ReadC();
    ((GrenadeInfo) hpSyncInfo).BoomInfo = obj0.ReadUH();
    ((GrenadeInfo) hpSyncInfo).WeaponId = obj0.ReadD();
    ((GrenadeInfo) hpSyncInfo).Accessory = obj0.ReadC();
    ((GrenadeInfo) hpSyncInfo).Extensions = obj0.ReadC();
    ((GrenadeInfo) hpSyncInfo).ObjPosX = obj0.ReadUH();
    ((GrenadeInfo) hpSyncInfo).ObjPosY = obj0.ReadUH();
    ((GrenadeInfo) hpSyncInfo).ObjPosZ = obj0.ReadUH();
    ((GrenadeInfo) hpSyncInfo).Unk5 = obj0.ReadD();
    ((GrenadeInfo) hpSyncInfo).Unk6 = obj0.ReadD();
    ((GrenadeInfo) hpSyncInfo).Unk7 = obj0.ReadD();
    GrenadeInfo grenadeInfo = (GrenadeInfo) hpSyncInfo;
    if (!obj2)
      grenadeInfo.WeaponClass = (ClassType) ComDiv.GetIdStatics(grenadeInfo.WeaponId, 2);
    if (Info)
      CLogger.Print($"UDP_SUB_HEAD_GRENADE; Weapon Id: {grenadeInfo.WeaponId}; Acc Flag: {grenadeInfo.Accessory}; Ext: {grenadeInfo.Extensions}; Boom Info: {grenadeInfo.BoomInfo}; X: {grenadeInfo.ObjPosX}; Y: {grenadeInfo.ObjPosY}; Z: {grenadeInfo.ObjPosZ}", LoggerType.Warning, (Exception) null);
    return grenadeInfo;
  }
}
