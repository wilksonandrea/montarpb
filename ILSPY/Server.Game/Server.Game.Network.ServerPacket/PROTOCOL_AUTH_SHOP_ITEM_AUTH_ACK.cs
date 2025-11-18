using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly ItemsModel itemsModel_0;

	public PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint uint_1, ItemsModel itemsModel_1 = null, Account account_0 = null)
	{
		itemsModel_0 = itemsModel_1;
		if (itemsModel_1 != null && uint_1 == 1)
		{
			int ıdStatics = ComDiv.GetIdStatics(itemsModel_1.Id, 1);
			if (ıdStatics != 17 && ıdStatics != 18 && ıdStatics != 20 && ıdStatics != 37)
			{
				itemsModel_1.Equip = ItemEquipType.Temporary;
			}
			else if (itemsModel_1.Count > 1 && itemsModel_1.Equip == ItemEquipType.Durable)
			{
				ComDiv.UpdateDB("player_items", "count", (long)(--itemsModel_1.Count), "object_id", itemsModel_1.ObjectId, "owner_id", account_0.PlayerId);
			}
			else
			{
				DaoManagerSQL.DeletePlayerInventoryItem(itemsModel_1.ObjectId, account_0.PlayerId);
				account_0.Inventory.RemoveItem(itemsModel_1);
				itemsModel_1.Id = 0;
				itemsModel_1.Count = 0u;
			}
		}
		else
		{
			uint_1 = 2147483648u;
		}
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1048);
		WriteD(uint_0);
		if (uint_0 == 1)
		{
			WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
			WriteD((uint)itemsModel_0.ObjectId);
			if (itemsModel_0.Category == ItemCategory.Coupon && itemsModel_0.Equip == ItemEquipType.Temporary)
			{
				WriteD(0);
				WriteC(1);
				WriteD(0);
			}
			else
			{
				WriteD(itemsModel_0.Id);
				WriteC((byte)itemsModel_0.Equip);
				WriteD(itemsModel_0.Count);
			}
		}
	}
}
