// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_INFO_LEAVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_INFO_LEAVE_ACK : GameServerPacket
{
  private byte[] method_0(Account uint_1, PlayerEquipment playerEquipment_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      RoomModel room = uint_1.Room;
      SlotModel slotModel;
      if (room != null && room.GetSlot(uint_1.SlotId, ref slotModel))
      {
        int num = room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam) == TeamEnum.FR_TEAM ? playerEquipment_1.CharaRedId : playerEquipment_1.CharaBlueId;
        syncServerPacket.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(num));
      }
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_1([In] PlayerEquipment obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      List<int> intList = new List<int>()
      {
        obj0.DinoItem,
        obj0.SprayId,
        obj0.NameCardId
      };
      if (intList.Count > 0)
      {
        syncServerPacket.WriteC((byte) intList.Count);
        foreach (int num in intList)
          syncServerPacket.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(num));
      }
      return syncServerPacket.ToArray();
    }
  }
}
