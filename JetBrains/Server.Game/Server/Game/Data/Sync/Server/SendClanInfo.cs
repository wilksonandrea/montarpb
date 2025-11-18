// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.SendClanInfo
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class SendClanInfo
{
  public SendClanInfo()
  {
  }

  public static void SendUDPPlayerSync(
    [In] RoomModel obj0,
    SlotModel Killer,
    CouponEffects Kill,
    int IsSuicide)
  {
    try
    {
      if (obj0 == null || Killer == null)
        return;
      PlayerEquipment equipment = Killer.Equipment;
      if (equipment == null)
      {
        CLogger.Print($"Slot Equipment Was Not Found! (UID: {Killer.PlayerId})", LoggerType.Warning, (Exception) null);
      }
      else
      {
        using (SyncServerPacket syncServerPacket = new SyncServerPacket())
        {
          syncServerPacket.WriteH((short) 1);
          syncServerPacket.WriteD(obj0.UniqueRoomId);
          syncServerPacket.WriteD(obj0.Seed);
          syncServerPacket.WriteQ(obj0.StartTick);
          syncServerPacket.WriteC((byte) IsSuicide);
          syncServerPacket.WriteC((byte) obj0.Rounds);
          syncServerPacket.WriteC((byte) Killer.Id);
          syncServerPacket.WriteC((byte) Killer.SpawnsCount);
          syncServerPacket.WriteC(BitConverter.GetBytes(Killer.PlayerId)[0]);
          if (IsSuicide == 0 || IsSuicide == 2)
          {
            int num = !obj0.IsDinoMode("") ? (obj0.ValidateTeam(Killer.Team, Killer.CostumeTeam) == TeamEnum.FR_TEAM ? equipment.CharaRedId : equipment.CharaBlueId) : (obj0.Rounds == 1 && Killer.Team == TeamEnum.CT_TEAM || obj0.Rounds == 2 && Killer.Team == TeamEnum.FR_TEAM ? (obj0.Rounds == 2 ? equipment.CharaRedId : equipment.CharaBlueId) : (obj0.TRex != Killer.Id ? equipment.DinoItem : -1));
            int Disposing = 0;
            if (Kill.HasFlag((Enum) CouponEffects.Ketupat))
              Disposing += 10;
            if (Kill.HasFlag((Enum) CouponEffects.HP5))
              Disposing += 5;
            if (Kill.HasFlag((Enum) CouponEffects.HP10))
              Disposing += 10;
            syncServerPacket.WriteD(num);
            syncServerPacket.WriteC((byte) Disposing);
            syncServerPacket.WriteC((byte) Kill.HasFlag((Enum) CouponEffects.C4SpeedKit));
            syncServerPacket.WriteD(equipment.WeaponPrimary);
            syncServerPacket.WriteD(equipment.WeaponSecondary);
            syncServerPacket.WriteD(equipment.WeaponMelee);
            syncServerPacket.WriteD(equipment.WeaponExplosive);
            syncServerPacket.WriteD(equipment.WeaponSpecial);
            syncServerPacket.WriteD(equipment.AccessoryId);
          }
          // ISSUE: reference to a compiler-generated method
          ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), obj0.UdpServer.Connection);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public SendClanInfo()
  {
  }

  public static void SendUDPRoundSync([In] RoomModel obj0)
  {
    try
    {
      if (obj0 == null)
        return;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 3);
        syncServerPacket.WriteD(obj0.UniqueRoomId);
        syncServerPacket.WriteD(obj0.Seed);
        syncServerPacket.WriteC((byte) obj0.Rounds);
        syncServerPacket.WriteC((byte) obj0.SwapRound);
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), obj0.UdpServer.Connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
