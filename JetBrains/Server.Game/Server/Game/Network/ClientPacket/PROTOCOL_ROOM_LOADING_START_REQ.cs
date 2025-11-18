// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_LOADING_START_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_LOADING_START_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room != null && ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).int_0 > 0 && ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).int_0 <= 8)
      {
        ChannelModel channel = player.GetChannel();
        if (channel != null)
        {
          using (PROTOCOL_SERVER_MESSAGE_INVITED_ACK messageInvitedAck = (PROTOCOL_SERVER_MESSAGE_INVITED_ACK) new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(player, room))
          {
            byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) messageInvitedAck).GetCompleteBytes("PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ");
            for (int index = 0; index < ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).int_0; ++index)
              ClanManager.GetAccount(channel.GetPlayer(((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).int_1).PlayerId, true)?.SendCompletePacket(completeBytes, messageInvitedAck.GetType().Name);
          }
        }
        else
          ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_JOIN_ACK(((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_JOIN_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_ROOM_JOIN_REQ) this).string_0 = this.ReadS(4);
    ((PROTOCOL_ROOM_JOIN_REQ) this).int_1 = (int) this.ReadC();
    int num = (int) this.ReadC();
  }
}
