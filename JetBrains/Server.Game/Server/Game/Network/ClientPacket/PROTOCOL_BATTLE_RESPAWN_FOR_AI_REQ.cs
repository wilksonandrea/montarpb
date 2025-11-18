// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ : GameClientPacket
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
      if (room == null)
        return;
      if (room.Stage == ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).stageOptions_0 && room.RoomType == ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).roomCondition_0 && room.MapId == ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).mapIdEnum_0 && room.Rule == ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).mapRules_0)
      {
        SlotModel slot = room.GetSlot(player.SlotId);
        if (slot != null && room.IsPreparing() && room.UdpServer != null && slot.State >= SlotState.LOAD)
        {
          Account leader = room.GetLeader();
          if (leader == null)
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(player, 0));
            room.ChangeSlotState(slot, SlotState.NORMAL, true);
            AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
            slot.StopTiming();
          }
          else if (string.IsNullOrEmpty(player.PublicIP.ToString()))
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK(EventErrorEnum.BATTLE_NO_REAL_IP));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(player, 0));
            room.ChangeSlotState(slot, SlotState.NORMAL, true);
            AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
            slot.StopTiming();
          }
          else
          {
            slot.PreStartDate = DateTimeUtil.Now();
            if (slot.Id == room.LeaderSlot)
            {
              room.State = RoomState.PRE_BATTLE;
              room.UpdateRoomInfo();
            }
            room.ChangeSlotState(slot, SlotState.PRESTART, true);
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_READYBATTLE_ACK(player, true));
            if (slot.Id != room.LeaderSlot)
              leader.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_READYBATTLE_ACK(player, false));
            room.StartCounter(1, player, slot);
          }
        }
        else
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_START_GAME_ACK());
          room.ChangeSlotState(slot, SlotState.NORMAL, true);
          AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
          slot.StopTiming();
        }
      }
      else
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK(EventErrorEnum.BATTLE_FIRST_MAINLOAD));
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RECORD_ACK());
        room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
        AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_PRESTARTBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_READYBATTLE_REQ) this).viewerType_0 = (ViewerType) this.ReadC();
    this.ReadD();
  }
}
