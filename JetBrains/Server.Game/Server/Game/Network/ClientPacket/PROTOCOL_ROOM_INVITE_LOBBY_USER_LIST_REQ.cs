// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ
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

public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ : GameClientPacket
{
  public virtual void Server\u002EGame\u002ENetwork\u002EGameClientPacket\u002ERun()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room != null && room.LeaderSlot == player.SlotId && room.State == RoomState.READY)
      {
        lock (room.Slots)
        {
          for (int index = 0; index < 18; ++index)
          {
            SlotModel slot = room.Slots[index];
            if (slot.PlayerId > 0L && index != room.LeaderSlot)
              ((Class2) this).list_0.Add(slot);
          }
        }
        if (((Class2) this).list_0.Count > 0)
        {
          SlotModel slotModel = ((Class2) this).list_0[new Random().Next(((Class2) this).list_0.Count)];
          ((Class2) this).uint_0 = room.GetPlayerBySlot(slotModel) != null ? (uint) slotModel.Id : 2147483648U /*0x80000000*/;
          ((Class2) this).list_0 = (List<SlotModel>) null;
        }
        else
          ((Class2) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      else
        ((Class2) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(((Class2) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_CHECK_MAIN_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ()
  {
    ((Class2) this).list_0 = new List<SlotModel>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    this.ReadD();
    ((PROTOCOL_ROOM_CREATE_REQ) this).string_0 = this.ReadU(46);
    ((PROTOCOL_ROOM_CREATE_REQ) this).mapIdEnum_0 = (MapIdEnum) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).mapRules_0 = (MapRules) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).stageOptions_0 = (StageOptions) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).roomCondition_0 = (RoomCondition) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).roomState_0 = (RoomState) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).int_3 = (int) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).roomWeaponsFlag_0 = (RoomWeaponsFlag) this.ReadH();
    ((PROTOCOL_ROOM_CREATE_REQ) this).roomStageFlag_0 = (RoomStageFlag) this.ReadD();
    int num1 = (int) this.ReadH();
    ((PROTOCOL_ROOM_CREATE_REQ) this).int_4 = this.ReadD();
    int num2 = (int) this.ReadH();
    ((PROTOCOL_ROOM_CREATE_REQ) this).string_2 = this.ReadU(66);
    ((PROTOCOL_ROOM_CREATE_REQ) this).int_2 = this.ReadD();
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_2 = this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_3 = this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).teamBalance_0 = (TeamBalance) this.ReadH();
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_0 = this.ReadB(24);
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_8 = this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_1 = this.ReadB(4);
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_7 = this.ReadC();
    int num3 = (int) this.ReadH();
    ((PROTOCOL_ROOM_CREATE_REQ) this).string_1 = this.ReadS(4);
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_4 = this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_5 = this.ReadC();
    ((PROTOCOL_ROOM_CREATE_REQ) this).byte_6 = this.ReadC();
  }
}
