// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.FireDataOnObject
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class FireDataOnObject
{
  public static void WriteInfo(SyncServerPacket S, DropWeaponInfo Hits)
  {
    if (ConfigLoader.UseMaxAmmoInDrop)
    {
      S.WriteH(ushort.MaxValue);
      S.WriteH(Hits.AmmoDual);
      S.WriteH((short) 10000);
    }
    else
    {
      S.WriteH(Hits.AmmoPrin);
      S.WriteH(Hits.AmmoDual);
      S.WriteH(Hits.AmmoTotal);
    }
    S.WriteH(Hits.Unk1);
    S.WriteH(Hits.Unk2);
    S.WriteH(Hits.Unk3);
    S.WriteC((byte) ((uint) Hits.Flags + (uint) Hits.Counter));
    S.WriteD(Hits.WeaponId);
    S.WriteC(Hits.Accessory);
    S.WriteC(Hits.Extensions);
  }

  public static FireDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    MissionDataInfo missionDataInfo = new MissionDataInfo();
    ((FireDataInfo) missionDataInfo).Effect = C.ReadC();
    ((FireDataInfo) missionDataInfo).Part = C.ReadC();
    ((FireDataInfo) missionDataInfo).Index = C.ReadH();
    ((FireDataInfo) missionDataInfo).WeaponId = C.ReadD();
    ((FireDataInfo) missionDataInfo).Accessory = C.ReadC();
    ((FireDataInfo) missionDataInfo).Extensions = C.ReadC();
    ((FireDataInfo) missionDataInfo).X = C.ReadUH();
    ((FireDataInfo) missionDataInfo).Y = C.ReadUH();
    ((FireDataInfo) missionDataInfo).Z = C.ReadUH();
    FireDataInfo fireDataInfo = (FireDataInfo) missionDataInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {fireDataInfo.WeaponId}; Extensions: {fireDataInfo.Extensions}; Fire: {fireDataInfo.Effect}; Part: {fireDataInfo.Part}; X: {fireDataInfo.X}; Y: {fireDataInfo.Y}; Z: {fireDataInfo.Z}", LoggerType.Warning, (Exception) null);
    return fireDataInfo;
  }
}
