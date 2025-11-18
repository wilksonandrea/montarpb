// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK : GameServerPacket
{
  private readonly long long_0;
  private readonly uint uint_0;

  public PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK([In] uint obj0, ItemsModel string_3, Account bool_1)
  {
    ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_0 = new List<ItemsModel>();
    ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_1 = new List<ItemsModel>();
    ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_2 = new List<ItemsModel>();
    ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_3 = new List<ItemsModel>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).uint_0 = obj0;
    ItemsModel itemsModel = new ItemsModel(string_3);
    if (itemsModel == null)
      return;
    ComDiv.TryCreateItem(itemsModel, bool_1.Inventory, bool_1.PlayerId);
    if (itemsModel.Category == ItemCategory.Weapon)
      ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_1.Add(itemsModel);
    else if (itemsModel.Category == ItemCategory.Character)
      ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_0.Add(itemsModel);
    else if (itemsModel.Category == ItemCategory.Coupon)
    {
      ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_2.Add(itemsModel);
    }
    else
    {
      if (itemsModel.Category != ItemCategory.NewItem)
        return;
      ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_3.Add(itemsModel);
    }
  }

  public virtual void Write()
  {
    this.WriteH((short) 1054);
    this.WriteD(((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).uint_0);
    if (((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).uint_0 != 1U)
      return;
    this.WriteH((short) 0);
    this.WriteH((ushort) (((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_0.Count + ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_1.Count + ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_2.Count + ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_3.Count));
    foreach (ItemsModel itemsModel in ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_0)
    {
      this.WriteD((uint) itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    foreach (ItemsModel itemsModel in ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_1)
    {
      this.WriteD((uint) itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    foreach (ItemsModel itemsModel in ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_2)
    {
      this.WriteD((uint) itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    foreach (ItemsModel itemsModel in ((PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK) this).list_3)
    {
      this.WriteD((uint) itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
  }
}
