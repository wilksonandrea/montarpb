// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly ItemsModel itemsModel_0;

  public PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint uint_1, List<GoodsItem> long_1 = 0, [In] Account obj2)
  {
    ((PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK) this).account_0 = obj2;
    ((PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK) this).list_0 = long_1;
  }

  public PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint shopData_1)
  {
    ((PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK) this).uint_0 = shopData_1;
  }
}
