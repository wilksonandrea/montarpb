// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_ITEMLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_ITEMLIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly ShopData shopData_0;

  public PROTOCOL_AUTH_SHOP_ITEMLIST_ACK([In] ShopData obj0, int int_2)
  {
    ((PROTOCOL_AUTH_SHOP_GOODSLIST_ACK) this).shopData_0 = obj0;
    ((PROTOCOL_AUTH_SHOP_GOODSLIST_ACK) this).int_0 = int_2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1036);
    this.WriteD(((PROTOCOL_AUTH_SHOP_GOODSLIST_ACK) this).int_0);
    this.WriteD(((PROTOCOL_AUTH_SHOP_GOODSLIST_ACK) this).shopData_0.ItemsCount);
    this.WriteD(((PROTOCOL_AUTH_SHOP_GOODSLIST_ACK) this).shopData_0.Offset);
    this.WriteB(((PROTOCOL_AUTH_SHOP_GOODSLIST_ACK) this).shopData_0.Buffer);
    this.WriteD(50);
  }
}
