// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ
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

public class PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ : GameClientPacket
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
      ChannelModel channelModel;
      if (room == null || room.GetLeader() == null || !room.GetChannel(ref channelModel))
        return;
      if (!room.IsPreparing())
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_START_GAME_ACK());
        room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
      }
      else
      {
        bool flag1 = room.IsBotMode();
        SlotModel slot1 = room.GetSlot(player.SlotId);
        if (slot1 != null && slot1.State == SlotState.PRESTART)
        {
          room.ChangeSlotState(slot1, SlotState.BATTLE_READY, true);
          slot1.StopTiming();
          if (flag1)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_DEATH_ACK(room));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_SLOT_ACK(room, flag1));
          int num1 = 0;
          int num2 = 0;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          int num6 = 0;
          foreach (SlotModel slot2 in room.Slots)
          {
            if (slot2.State >= SlotState.LOAD)
            {
              ++num4;
              if (slot2.Team == TeamEnum.FR_TEAM)
                ++num5;
              else
                ++num6;
              if (slot2.State >= SlotState.BATTLE_READY)
              {
                ++num1;
                if (slot2.Team == TeamEnum.FR_TEAM)
                  ++num3;
                else
                  ++num2;
              }
            }
          }
          int num7 = room.State == RoomState.BATTLE ? 1 : 0;
          bool flag2 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY & flag1 && (room.LeaderSlot % 2 == 0 && num3 > num5 / 2 || room.LeaderSlot % 2 == 1 && num2 > num6 / 2);
          bool flag3 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && num2 > num6 / 2 && num3 > num5 / 2;
          bool flag4 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && room.RoomType == RoomCondition.FreeForAll && num1 >= 2 && num4 >= 2;
          bool flag5 = channelModel.Type == ChannelType.Clan && num1 == num3 + num2;
          bool flag6 = room.Competitive && num1 == num3 + num2;
          int num8 = flag2 ? 1 : 0;
          if ((num7 | num8 | (flag3 ? 1 : 0) | (flag4 ? 1 : 0) | (flag5 ? 1 : 0) | (flag6 ? 1 : 0)) == 0)
            return;
          if (flag5)
            CLogger.Print($"Starting Clan War Match with '{num1}' players. FR: {num3} CT: {num2}", LoggerType.Warning, (Exception) null);
          if (flag6)
            CLogger.Print($"Starting Competitive Match with '{num1}' players. FR: {num3} CT: {num2}", LoggerType.Warning, (Exception) null);
          room.SpawnReadyPlayers();
        }
        else
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(player, 0));
          room.ChangeSlotState(slot1, SlotState.NORMAL, true);
          AllUtils.BattleEndPlayersCount(room, flag1);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_STARTBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_0 = (int) this.ReadC();
  }
}
