// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_INFO_ENTER_REQ
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

public class PROTOCOL_ROOM_INFO_ENTER_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      Account State;
      if (room == null || player.SlotId == ((PROTOCOL_ROOM_GET_PLAYERINFO_REQ) this).int_0 || !room.GetPlayerBySlot(((PROTOCOL_ROOM_GET_PLAYERINFO_REQ) this).int_0, ref State))
        return;
      uint channelModel_0 = 0;
      int[] numArray = new int[2];
      SlotModel slot = room.GetSlot(State.SlotId);
      if (slot != null)
      {
        switch (room.ValidateTeam(slot.Team, slot.CostumeTeam))
        {
          case TeamEnum.FR_TEAM:
            numArray[0] = slot.Equipment.CharaRedId;
            break;
          case TeamEnum.CT_TEAM:
            numArray[0] = slot.Equipment.CharaBlueId;
            break;
        }
        numArray[1] = slot.Equipment.AccessoryId;
      }
      else
        channelModel_0 = 134217728U /*0x08000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_SLOTINFO_ACK(channelModel_0, State, numArray));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ) this).int_0 = (int) this.ReadC();
  }
}
