// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Managers;
using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1040);
    this.WriteD(((PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK) this).int_0);
    this.WriteD(((PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK) this).shopData_0.ItemsCount);
    this.WriteD(((PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK) this).shopData_0.Offset);
    this.WriteB(((PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK) this).shopData_0.Buffer);
    this.WriteB(ShopManager.ShopTagData);
  }

  public PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(MessageModel string_1)
  {
    ((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0 = string_1;
  }
}
