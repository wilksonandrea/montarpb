// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.GrenadeHit
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

public class GrenadeHit
{
  public static void WriteInfo(SyncServerPacket S, WeaponClient Info)
  {
    if (ConfigLoader.UseMaxAmmoInDrop)
    {
      S.WriteH(ushort.MaxValue);
      S.WriteH(Info.AmmoDual);
      S.WriteH((short) 10000);
    }
    else
    {
      S.WriteH(Info.AmmoPrin);
      S.WriteH(Info.AmmoDual);
      S.WriteH(Info.AmmoTotal);
    }
    S.WriteH(Info.Unk1);
    S.WriteH(Info.Unk2);
    S.WriteH(Info.Unk3);
    S.WriteC(Info.Flags);
    S.WriteD(Info.WeaponId);
    S.WriteC(Info.Accessory);
    S.WriteC(Info.Extensions);
  }

  public static WeaponHost ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    AssistManager assistManager = new AssistManager();
    ((WeaponHost) assistManager).DeathType = C.ReadC();
    ((WeaponHost) assistManager).X = C.ReadUH();
    ((WeaponHost) assistManager).Y = C.ReadUH();
    ((WeaponHost) assistManager).Z = C.ReadUH();
    ((WeaponHost) assistManager).Unks = C.ReadC();
    ((WeaponHost) assistManager).UNK = C.ReadD();
    ((WeaponHost) assistManager).HitPart = C.ReadC();
    WeaponHost weaponHost = (WeaponHost) assistManager;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; UNK: {weaponHost.UNK}; Death Type: {weaponHost.DeathType}; Hit: {weaponHost.HitPart}; X: {weaponHost.X}; Y: {weaponHost.Y}; Z: {weaponHost.Z}", LoggerType.Warning, (Exception) null);
    return weaponHost;
  }
}
