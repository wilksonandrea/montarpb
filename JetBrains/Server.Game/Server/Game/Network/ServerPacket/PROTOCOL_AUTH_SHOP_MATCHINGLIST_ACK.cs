// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly ShopData shopData_0;

  public virtual void Write()
  {
    this.WriteH((short) 1048);
    this.WriteD(((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).uint_0);
    if (((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).uint_0 != 1U)
      return;
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
    this.WriteD((uint) ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0.ObjectId);
    if (((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0.Category == ItemCategory.Coupon && ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0.Equip == ItemEquipType.Temporary)
    {
      this.WriteD(0);
      this.WriteC((byte) 1);
      this.WriteD(0);
    }
    else
    {
      this.WriteD(((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0.Id);
      this.WriteC((byte) ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0.Equip);
      this.WriteD(((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0.Count);
    }
  }

  public PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(uint shopData_1)
  {
    ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK) this).uint_0 = shopData_1;
  }
}
