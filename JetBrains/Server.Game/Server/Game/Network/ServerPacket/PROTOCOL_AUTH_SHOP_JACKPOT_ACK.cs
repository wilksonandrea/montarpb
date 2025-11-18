// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_JACKPOT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_JACKPOT_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 1038);
    this.WriteD(((PROTOCOL_AUTH_SHOP_ITEMLIST_ACK) this).int_0);
    this.WriteD(((PROTOCOL_AUTH_SHOP_ITEMLIST_ACK) this).shopData_0.ItemsCount);
    this.WriteD(((PROTOCOL_AUTH_SHOP_ITEMLIST_ACK) this).shopData_0.Offset);
    this.WriteB(((PROTOCOL_AUTH_SHOP_ITEMLIST_ACK) this).shopData_0.Buffer);
    this.WriteD(800);
  }

  public PROTOCOL_AUTH_SHOP_JACKPOT_ACK([In] uint obj0, ItemsModel list_1, Account account_1)
  {
    ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).itemsModel_0 = list_1;
    if (list_1 != null && obj0 == 1U)
    {
      switch (ComDiv.GetIdStatics(list_1.Id, 1))
      {
        case 17:
        case 18:
        case 20:
        case 37:
          if (list_1.Count > 1U && list_1.Equip == ItemEquipType.Durable)
          {
            ComDiv.UpdateDB("player_items", "count", (object) (long) --list_1.Count, "object_id", (object) list_1.ObjectId, "owner_id", (object) account_1.PlayerId);
            break;
          }
          DaoManagerSQL.DeletePlayerInventoryItem(list_1.ObjectId, account_1.PlayerId);
          account_1.Inventory.RemoveItem(list_1);
          list_1.Id = 0;
          list_1.Count = 0U;
          break;
        default:
          list_1.Equip = ItemEquipType.Temporary;
          break;
      }
    }
    else
      obj0 = 2147483648U /*0x80000000*/;
    ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK) this).uint_0 = obj0;
  }
}
