// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room == null || room.State < RoomState.LOADING || !room.GetSlot(player.SlotId, ref slotModel) || slotModel.State < SlotState.LOAD)
        return;
      bool Error = room.IsBotMode();
      AllUtils.FreepassEffect(player, slotModel, room, Error);
      if (room.VoteTime.IsTimer() && room.VoteKick != null && room.VoteKick.VictimIdx == slotModel.Id)
      {
        room.VoteTime.StopJob();
        room.VoteKick = (VoteKickModel) null;
        using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK kickvoteCancelAck = (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK) new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK())
          room.SendPacketToPlayers((GameServerPacket) kickvoteCancelAck, SlotState.BATTLE, 0, slotModel.Id);
      }
      AllUtils.ResetSlotInfo(room, slotModel, true);
      int TeamFR = 0;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      foreach (SlotModel slot in room.Slots)
      {
        if (slot.State >= SlotState.LOAD)
        {
          if (slot.Team == TeamEnum.FR_TEAM)
            ++num2;
          else
            ++num3;
          if (slot.State == SlotState.BATTLE)
          {
            if (slot.Team == TeamEnum.FR_TEAM)
              ++TeamFR;
            else
              ++num1;
          }
        }
      }
      if (slotModel.Id == room.LeaderSlot)
      {
        if (Error)
        {
          if (TeamFR <= 0 && num1 <= 0)
            AllUtils.LeaveHostEndBattlePVE(room, player);
          else
            AllUtils.LeaveHostGiveBattlePVE(room, player);
        }
        else if (room.State == RoomState.BATTLE && (TeamFR == 0 || num1 == 0) || room.State <= RoomState.PRE_BATTLE && (num2 == 0 || num3 == 0))
          AllUtils.LeaveHostEndBattlePVP(room, player, TeamFR, num1, ref ((PROTOCOL_BATTLE_GIVEUPBATTLE_REQ) this).bool_0);
        else
          AllUtils.LeaveHostGiveBattlePVP(room, player);
      }
      else if (!Error)
      {
        if (room.State == RoomState.BATTLE && (TeamFR == 0 || num1 == 0) || room.State <= RoomState.PRE_BATTLE && (num2 == 0 || num3 == 0))
          AllUtils.LeavePlayerEndBattlePVP(room, player, TeamFR, num1, out ((PROTOCOL_BATTLE_GIVEUPBATTLE_REQ) this).bool_0);
        else
          AllUtils.LeavePlayerQuitBattle(room, player);
      }
      else
        AllUtils.LeavePlayerQuitBattle(room, player);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(player, 0));
      if (((PROTOCOL_BATTLE_GIVEUPBATTLE_REQ) this).bool_0 || room.State != RoomState.BATTLE)
        return;
      AllUtils.BattleEndRoundPlayersCount(room);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).byte_0 = this.ReadC();
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).int_1 = this.ReadD();
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).float_0 = this.ReadT();
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).float_1 = this.ReadT();
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).float_2 = this.ReadT();
  }
}
