// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ : GameClientPacket
{
  private int int_0;

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
      RoomModel room = player.Room;
      if (room != null)
      {
        if (room.State != RoomState.READY || room.LeaderSlot == player.SlotId)
          return;
        List<Account> allPlayers = room.GetAllPlayers();
        if (allPlayers.Count == 0)
          return;
        if (player.IsGM())
        {
          this.method_0(room, allPlayers, player.SlotId);
        }
        else
        {
          if (!room.RequestRoomMaster.Contains(player.PlayerId))
          {
            room.RequestRoomMaster.Add(player.PlayerId);
            if (room.RequestRoomMaster.Count() < allPlayers.Count / 2 + 1)
            {
              using (PROTOCOL_ROOM_REQUEST_MAIN_ACK roomRequestMainAck = (PROTOCOL_ROOM_REQUEST_MAIN_ACK) new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(player.SlotId))
                ((PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ) this).method_1((GameServerPacket) roomRequestMainAck, allPlayers);
            }
          }
          if (room.RequestRoomMaster.Count() < allPlayers.Count / 2 + 1)
            return;
          this.method_0(room, allPlayers, player.SlotId);
        }
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(2147483648U /*0x80000000*/));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0([In] RoomModel obj0, List<Account> roomModel_0, int slotModel_0)
  {
    obj0.SetNewLeader(slotModel_0, SlotState.EMPTY, -1, false);
    using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK mainChangeWhoAck = (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK) new PROTOCOL_ROOM_TEAM_BALANCE_ACK(slotModel_0))
      ((PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ) this).method_1((GameServerPacket) mainChangeWhoAck, roomModel_0);
    obj0.UpdateSlotsInfo();
    obj0.RequestRoomMaster.Clear();
  }
}
