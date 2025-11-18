// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK : GameServerPacket
{
  private readonly List<GoodsItem> list_0;
  private readonly Account account_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1056);
    this.WriteD(((PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK) this).uint_0);
    if (((PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK) this).uint_0 != 1U)
      return;
    this.WriteD((int) ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK) this).long_0);
  }

  public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint list_1)
  {
    ((PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK) this).uint_0 = list_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1042);
    this.WriteH((short) 0);
  }
}
