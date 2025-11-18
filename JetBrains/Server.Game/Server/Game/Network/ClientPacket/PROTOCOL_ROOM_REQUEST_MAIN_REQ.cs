// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_REQUEST_MAIN_REQ
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

public class PROTOCOL_ROOM_REQUEST_MAIN_REQ : GameClientPacket
{
  public virtual void Server\u002EGame\u002ENetwork\u002EGameClientPacket\u002ERun()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.LeaderSlot != player.SlotId || room.State != RoomState.READY)
        return;
      lock (room.Slots)
      {
        for (int index = 0; index < 18; ++index)
        {
          SlotModel slot = room.Slots[index];
          if (slot.PlayerId > 0L && index != room.LeaderSlot)
            ((Class3) this).list_0.Add(slot);
        }
      }
      if (((Class3) this).list_0.Count > 0)
      {
        SlotModel slotModel = ((Class3) this).list_0[new Random().Next(((Class3) this).list_0.Count)];
        if (room.GetPlayerBySlot(slotModel) != null)
        {
          room.SetNewLeader(slotModel.Id, SlotState.EMPTY, room.LeaderSlot, false);
          using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK Player = (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK) new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK(slotModel.Id))
            room.SendPacketToPlayers((GameServerPacket) Player);
          room.UpdateSlotsInfo();
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(2147483648U /*0x80000000*/));
        ((Class3) this).list_0 = (List<SlotModel>) null;
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(2147483648U /*0x80000000*/));
    }
    catch (Exception ex)
    {
      CLogger.Print("ROOM_RANDOM_HOST2_REC: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_ROOM_REQUEST_MAIN_REQ()
  {
    ((Class3) this).list_0 = new List<SlotModel>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ) this).int_0 = this.ReadD();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room != null && room.LeaderSlot != ((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ) this).int_0 && room.Slots[((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ) this).int_0].PlayerId != 0L)
      {
        if (room.State != RoomState.READY || room.LeaderSlot != player.SlotId)
          return;
        room.SetNewLeader(((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ) this).int_0, SlotState.EMPTY, room.LeaderSlot, false);
        using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK Player = (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK) new PROTOCOL_ROOM_TEAM_BALANCE_ACK(((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ) this).int_0))
          room.SendPacketToPlayers((GameServerPacket) Player);
        room.UpdateSlotsInfo();
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_TEAM_BALANCE_ACK(2147483648U /*0x80000000*/));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_ROOM_REQUEST_MAIN_REQ()
  {
  }
}
