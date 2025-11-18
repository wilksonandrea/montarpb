// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_INVENTORY_LEAVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_INVENTORY_LEAVE_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3330);
    this.WriteD(0);
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
  }

  public PROTOCOL_INVENTORY_LEAVE_ACK(int uint_1, Account long_0, [In] List<GoodsItem> obj2)
  {
    ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).int_0 = uint_1;
    if (long_0 != null)
    {
      ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).playerInventory_0 = long_0.Inventory;
      ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0 = new List<ItemsModel>();
    }
    foreach (GoodsItem goodsItem in obj2)
    {
      ItemsModel Type = new ItemsModel(goodsItem.Item);
      if (Type != null)
      {
        if (uint_1 == 0)
          ComDiv.TryCreateItem(Type, long_0.Inventory, long_0.PlayerId);
        UpdateServer.LoadItem(long_0, Type);
        ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0.Add(Type);
      }
    }
  }
}
