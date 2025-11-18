// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_LOBBY_GET_ROOMLIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.LastLobbyEnter = DateTimeUtil.Now();
      if (player.ChannelId >= 0)
        player.GetChannel()?.AddPlayer(player.Session);
      RoomModel room = player.Room;
      if (room != null)
      {
        if (player.SlotId >= 0 && room.State >= RoomState.LOADING && room.Slots[player.SlotId].State >= SlotState.LOAD)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(0U));
          player.LastLobbyEnter = DateTimeUtil.Now();
          return;
        }
        room.RemovePlayer(player, false, 0);
      }
      AllUtils.SyncPlayerToFriends(player, false);
      AllUtils.SyncPlayerToClanMembers(player);
      AllUtils.GetXmasReward(player);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(0U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_LOBBY_GET_ROOMLIST_REQ()
  {
  }

  public virtual void Read() => ((PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ) this).int_0 = this.ReadD();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ChannelModel channel = player.GetChannel();
      if (channel == null)
        return;
      RoomModel room = ((MatchModel) channel).GetRoom(((PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ) this).int_0);
      if (room == null || room.GetLeader() == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_LEAVE_ACK(room));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_LOBBY_GET_ROOMLIST_REQ()
  {
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ChannelModel channel = player.GetChannel();
      if (channel == null)
        return;
      channel.RemoveEmptyRooms();
      List<RoomModel> rooms = channel.Rooms;
      List<Account> waitPlayers = ((MatchModel) channel).GetWaitPlayers();
      int num1 = (int) Math.Ceiling((double) rooms.Count / 10.0);
      int num2 = (int) Math.Ceiling((double) waitPlayers.Count / 8.0);
      if (player.LastRoomPage >= num1)
        player.LastRoomPage = 0;
      if (player.LastPlayerPage >= num2)
        player.LastPlayerPage = 0;
      int account_0 = 0;
      int int_1 = 0;
      byte[] numArray1 = ((PROTOCOL_LOBBY_LEAVE_REQ) this).method_0(player.LastRoomPage, ref account_0, rooms);
      byte[] numArray2 = ((PROTOCOL_LOBBY_LEAVE_REQ) this).method_2(player.LastPlayerPage, ref int_1, waitPlayers);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_NEW_MYINFO_ACK(rooms.Count, waitPlayers.Count, player.LastRoomPage++, player.LastPlayerPage++, account_0, int_1, numArray1, numArray2));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_GET_ROOMLIST_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
