// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_GOODSLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GOODSLIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly ShopData shopData_0;

  public virtual void Write()
  {
    this.WriteH((short) 1064);
    this.WriteH((short) 0);
    this.WriteC((byte) 1);
    this.WriteC((byte) ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).list_0.Count);
    foreach (ItemsModel itemsModel in ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).list_0)
      this.WriteD(itemsModel.Id);
  }

  public PROTOCOL_AUTH_SHOP_GOODSLIST_ACK([In] uint obj0, long int_2)
  {
    ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK) this).uint_0 = obj0;
    if (obj0 != 1U)
      return;
    ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK) this).long_0 = int_2;
  }
}
