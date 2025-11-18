// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK([In] ItemsModel obj0, int string_3, int uint_1)
  {
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).int_0 = string_3;
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).int_1 = uint_1;
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).list_0 = new List<ItemsModel>();
    ItemsModel itemsModel = new ItemsModel(obj0);
    if (itemsModel == null)
      return;
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).list_0.Add(itemsModel);
  }

  public PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(
    [In] List<ItemsModel> obj0,
    int itemsModel_0 = default (int),
    int account_0 = default (int))
  {
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).int_0 = itemsModel_0;
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).int_1 = account_0;
    ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).list_0 = new List<ItemsModel>();
    foreach (ItemsModel itemsModel1 in obj0)
    {
      ItemsModel itemsModel2 = new ItemsModel(itemsModel1);
      if (itemsModel2 != null)
        ((PROTOCOL_AUTH_SHOP_CAPSULE_ACK) this).list_0.Add(itemsModel2);
    }
  }
}
