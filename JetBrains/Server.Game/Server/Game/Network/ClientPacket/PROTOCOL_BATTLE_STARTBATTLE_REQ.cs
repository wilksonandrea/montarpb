// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_STARTBATTLE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_STARTBATTLE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.State != RoomState.BATTLE)
        return;
      SlotModel slot = room.GetSlot(player.SlotId);
      if (slot == null || slot.State != SlotState.BATTLE)
        return;
      if (slot.DeathState.HasFlag((Enum) DeadEnum.Dead) || slot.DeathState.HasFlag((Enum) DeadEnum.UseChat))
        slot.DeathState = DeadEnum.Alive;
      PlayerEquipment playerEquipment = AllUtils.ValidateRespawnEQ(slot, ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0);
      if (playerEquipment != null)
      {
        ComDiv.CheckEquipedItems(playerEquipment, player.Inventory.Items, true);
        AllUtils.ClassicModeCheck(room, playerEquipment);
        slot.Equipment = playerEquipment;
        if ((((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1 & 8) > 0)
          AllUtils.InsertItem(playerEquipment.WeaponPrimary, slot);
        if ((((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1 & 4) > 0)
          AllUtils.InsertItem(playerEquipment.WeaponSecondary, slot);
        if ((((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1 & 2) > 0)
          AllUtils.InsertItem(playerEquipment.WeaponMelee, slot);
        if ((((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1 & 1) > 0)
          AllUtils.InsertItem(playerEquipment.WeaponExplosive, slot);
        AllUtils.InsertItem(playerEquipment.WeaponSpecial, slot);
        AllUtils.InsertItem(playerEquipment.PartHead, slot);
        AllUtils.InsertItem(playerEquipment.PartFace, slot);
        AllUtils.InsertItem(playerEquipment.BeretItem, slot);
        AllUtils.InsertItem(playerEquipment.AccessoryId, slot);
        int idStatics1 = ComDiv.GetIdStatics(((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[5], 1);
        int idStatics2 = ComDiv.GetIdStatics(((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[5], 2);
        int idStatics3 = ComDiv.GetIdStatics(((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[5], 5);
        switch (idStatics1)
        {
          case 6:
            if (idStatics2 != 1 && idStatics3 != 632)
            {
              if (idStatics2 == 2 || idStatics3 == 664)
              {
                AllUtils.InsertItem(playerEquipment.CharaBlueId, slot);
                break;
              }
              break;
            }
            AllUtils.InsertItem(playerEquipment.CharaRedId, slot);
            break;
          case 15:
            AllUtils.InsertItem(playerEquipment.DinoItem, slot);
            break;
        }
      }
      using (PROTOCOL_BATTLE_RESPAWN_ACK battleRespawnAck = (PROTOCOL_BATTLE_RESPAWN_ACK) new PROTOCOL_BATTLE_SENDPING_ACK(room, slot))
        room.SendPacketToPlayers((GameServerPacket) battleRespawnAck, SlotState.BATTLE, 0);
      if (slot.FirstRespawn)
      {
        slot.FirstRespawn = false;
        SendClanInfo.SendUDPPlayerSync(room, slot, player.Effects, 0);
      }
      else
        SendClanInfo.SendUDPPlayerSync(room, slot, player.Effects, 2);
      if (room.WeaponsFlag == (RoomWeaponsFlag) ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1)
        return;
      CLogger.Print($"Player '{player.Nickname}' Weapon Flags Doesn't Match! (Room: {(int) room.WeaponsFlag}; Player: {((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1})", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_RESPAWN_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_SENDPING_REQ) this).byte_0 = this.ReadB(16 /*0x10*/);
  }
}
