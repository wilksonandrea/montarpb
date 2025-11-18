// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ : GameClientPacket
{
  private int int_0;
  private float float_0;
  private float float_1;
  private float float_2;
  private byte byte_0;
  private int int_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.RoundTime.IsTimer() || room.State < RoomState.BATTLE)
        return;
      bool Score = room.IsBotMode();
      SlotModel slot = room.GetSlot((int) ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0.KillerSlot);
      if (slot == null || !Score && (slot.State < SlotState.BATTLE || slot.Id != player.SlotId))
        return;
      int num;
      RoomHitMarker.RegistryFragInfos(room, slot, ref num, Score, ((PROTOCOL_BATTLE_DEATH_REQ) this).bool_0, ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0);
      if (Score)
      {
        slot.Score += slot.KillsOnLife + (int) room.IngameAiLevel + num;
        if (slot.Score > (int) ushort.MaxValue)
        {
          slot.Score = (int) ushort.MaxValue;
          AllUtils.ValidateBanPlayer(player, $"AI Score Cheating! ({slot.Score})");
        }
        ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0.Score = slot.Score;
      }
      else
      {
        slot.Score += num;
        AllUtils.CompleteMission(room, player, slot, ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0, MissionType.NA, 0);
        ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0.Score = num;
      }
      using (PROTOCOL_BATTLE_DEATH_ACK protocolBattleDeathAck = (PROTOCOL_BATTLE_DEATH_ACK) new PROTOCOL_BATTLE_ENDBATTLE_ACK(room, ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0, slot))
        room.SendPacketToPlayers((GameServerPacket) protocolBattleDeathAck, SlotState.BATTLE, 0);
      RoomPassPortal.EndBattleByDeath(room, slot, Score, ((PROTOCOL_BATTLE_DEATH_REQ) this).bool_0, ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_DEATH_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_GIVEUPBATTLE_REQ) this).long_0 = (long) this.ReadD();
  }
}
