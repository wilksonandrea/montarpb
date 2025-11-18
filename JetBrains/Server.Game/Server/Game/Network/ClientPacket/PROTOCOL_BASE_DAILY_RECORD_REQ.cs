// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_DAILY_RECORD_REQ
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

public class PROTOCOL_BASE_DAILY_RECORD_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || string.IsNullOrEmpty(((PROTOCOL_BASE_CHATTING_REQ) this).string_0) || ((PROTOCOL_BASE_CHATTING_REQ) this).string_0.Length > 60 || player.Nickname.Length == 0)
        return;
      RoomModel room1 = player.Room;
      switch (((PROTOCOL_BASE_CHATTING_REQ) this).chattingType_0)
      {
        case ChattingType.All:
        case ChattingType.Lobby:
          if (room1 != null)
          {
            if (AllUtils.ServerCommands(player, ((PROTOCOL_BASE_CHATTING_REQ) this).string_0))
              break;
            SlotModel slot1 = room1.Slots[player.SlotId];
            using (PROTOCOL_ROOM_CHATTING_ACK protocolRoomChattingAck = (PROTOCOL_ROOM_CHATTING_ACK) new PROTOCOL_ROOM_CREATE_ACK((int) ((PROTOCOL_BASE_CHATTING_REQ) this).chattingType_0, slot1.Id, player.UseChatGM(), ((PROTOCOL_BASE_CHATTING_REQ) this).string_0))
            {
              byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) protocolRoomChattingAck).GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-2");
              lock (room1.Slots)
              {
                foreach (SlotModel slot2 in room1.Slots)
                {
                  Account playerBySlot = room1.GetPlayerBySlot(slot2);
                  if (playerBySlot != null && AllUtils.SlotValidMessage(slot1, slot2))
                    playerBySlot.SendCompletePacket(completeBytes, protocolRoomChattingAck.GetType().Name);
                }
                break;
              }
            }
          }
          ChannelModel channel = player.GetChannel();
          if (channel == null || AllUtils.ServerCommands(player, ((PROTOCOL_BASE_CHATTING_REQ) this).string_0))
            break;
          using (PROTOCOL_LOBBY_CHATTING_ACK room2 = (PROTOCOL_LOBBY_CHATTING_ACK) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(player, ((PROTOCOL_BASE_CHATTING_REQ) this).string_0, false))
          {
            ((MatchModel) channel).SendPacketToWaitPlayers((GameServerPacket) room2);
            break;
          }
        case ChattingType.Team:
          if (room1 == null)
            break;
          SlotModel slot3 = room1.Slots[player.SlotId];
          int[] teamArray = room1.GetTeamArray(slot3.Team);
          using (PROTOCOL_ROOM_CHATTING_ACK protocolRoomChattingAck = (PROTOCOL_ROOM_CHATTING_ACK) new PROTOCOL_ROOM_CREATE_ACK((int) ((PROTOCOL_BASE_CHATTING_REQ) this).chattingType_0, slot3.Id, player.UseChatGM(), ((PROTOCOL_BASE_CHATTING_REQ) this).string_0))
          {
            byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) protocolRoomChattingAck).GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-1");
            lock (room1.Slots)
            {
              foreach (int index in teamArray)
              {
                SlotModel slot4 = room1.Slots[index];
                if (slot4 != null)
                {
                  Account playerBySlot = room1.GetPlayerBySlot(slot4);
                  if (playerBySlot != null && AllUtils.SlotValidMessage(slot3, slot4))
                    playerBySlot.SendCompletePacket(completeBytes, protocolRoomChattingAck.GetType().Name);
                }
              }
              break;
            }
          }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }
}
