// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ : GameClientPacket
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
      if (room == null || ((PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ) this).teamEnum_0 == TeamEnum.ALL_TEAM && ((PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ) this).teamEnum_0 == TeamEnum.TEAM_DRAW || room.ChangingSlots)
        return;
      SlotModel slot1 = room.GetSlot(player.SlotId);
      if (slot1 == null || slot1.State != SlotState.NORMAL)
        return;
      room.ChangingSlots = true;
      SlotModel slot2 = room.GetSlot(((PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ) this).int_0);
      if (slot2 == null || slot2.Team != ((PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ) this).teamEnum_0 || slot2.PlayerId != 0L || slot2.State != SlotState.EMPTY)
        return;
      List<SlotChange> int_0 = new List<SlotChange>();
      room.SwitchSlots(int_0, player, slot1, slot2, SlotState.NORMAL);
      if (int_0.Count > 0)
      {
        using (PROTOCOL_ROOM_TEAM_BALANCE_ACK Player = (PROTOCOL_ROOM_TEAM_BALANCE_ACK) new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(int_0, room.LeaderSlot, 0))
          room.SendPacketToPlayers((GameServerPacket) Player);
      }
      room.ChangingSlots = false;
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name}: {ex.Message}", LoggerType.Error, ex);
    }
  }

  public virtual void Server\u002EGame\u002ENetwork\u002EGameClientPacket\u002ERead()
  {
  }
}
