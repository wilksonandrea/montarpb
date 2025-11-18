// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK : GameServerPacket
{
  private readonly uint uint_0;

  private byte[] method_2()
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      List<ItemsModel> itemsByType = ((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.GetItemsByType(ItemCategory.Coupon);
      if (itemsByType.Count > 0)
      {
        syncServerPacket.WriteH((short) itemsByType.Count);
        foreach (ItemsModel itemsModel in itemsByType)
          syncServerPacket.WriteD(itemsModel.Id);
      }
      return syncServerPacket.ToArray();
    }
  }
}
