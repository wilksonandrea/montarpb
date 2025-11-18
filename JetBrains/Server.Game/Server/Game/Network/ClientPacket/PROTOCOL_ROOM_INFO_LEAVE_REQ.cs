// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_INFO_LEAVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_INFO_LEAVE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room == null || player.SlotId == ((PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ) this).int_0 || !room.GetSlot(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ) this).int_0, ref slotModel))
        return;
      uint roomModel_1 = 0;
      int[] numArray = new int[2];
      PlayerEquipment equipment = slotModel.Equipment;
      if (equipment != null)
      {
        switch (room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam))
        {
          case TeamEnum.FR_TEAM:
            numArray[0] = equipment.CharaRedId;
            break;
          case TeamEnum.CT_TEAM:
            numArray[0] = equipment.CharaBlueId;
            break;
        }
        numArray[1] = equipment.AccessoryId;
      }
      else
        roomModel_1 = 134217728U /*0x08000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_USER_ITEM_ACK(roomModel_1, equipment, numArray, (byte) slotModel.Id));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
