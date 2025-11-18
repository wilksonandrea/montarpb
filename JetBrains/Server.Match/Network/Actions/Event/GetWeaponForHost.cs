// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.GetWeaponForHost
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class GetWeaponForHost
{
  public static void WriteInfo(SyncServerPacket S, FireNHitDataObjectInfo Info)
  {
    S.WriteH(Info.Portal);
  }

  public static WeaponClient ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    AssistManager assistManager = new AssistManager();
    ((WeaponClient) assistManager).AmmoPrin = C.ReadUH();
    ((WeaponClient) assistManager).AmmoDual = C.ReadUH();
    ((WeaponClient) assistManager).AmmoTotal = C.ReadUH();
    ((WeaponClient) assistManager).Unk1 = C.ReadUH();
    ((WeaponClient) assistManager).Unk2 = C.ReadUH();
    ((WeaponClient) assistManager).Unk3 = C.ReadUH();
    ((WeaponClient) assistManager).Flags = C.ReadC();
    ((WeaponClient) assistManager).WeaponId = C.ReadD();
    ((WeaponClient) assistManager).Accessory = C.ReadC();
    ((WeaponClient) assistManager).Extensions = C.ReadC();
    WeaponClient weaponClient = (WeaponClient) assistManager;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {weaponClient.WeaponId}; Ext: {weaponClient.Extensions}; Acc: {weaponClient.Accessory}; Ammo Prin: {weaponClient.AmmoPrin}; Ammo Dual: {weaponClient.AmmoDual}; Ammo Total: {weaponClient.AmmoTotal}", LoggerType.Warning, (Exception) null);
    return weaponClient;
  }
}
