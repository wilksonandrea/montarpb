using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly List<ItemsModel> list_0 = new List<ItemsModel>();

	private readonly List<ItemsModel> list_1 = new List<ItemsModel>();

	private readonly List<ItemsModel> list_2 = new List<ItemsModel>();

	private readonly List<ItemsModel> list_3 = new List<ItemsModel>();

	public PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(uint uint_1, ItemsModel itemsModel_0 = null, Account account_0 = null)
	{
		uint_0 = uint_1;
		ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
		if (ıtemsModel != null)
		{
			ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
			if (ıtemsModel.Category == ItemCategory.Weapon)
			{
				list_1.Add(ıtemsModel);
			}
			else if (ıtemsModel.Category == ItemCategory.Character)
			{
				list_0.Add(ıtemsModel);
			}
			else if (ıtemsModel.Category == ItemCategory.Coupon)
			{
				list_2.Add(ıtemsModel);
			}
			else if (ıtemsModel.Category == ItemCategory.NewItem)
			{
				list_3.Add(ıtemsModel);
			}
		}
	}

	public override void Write()
	{
		WriteH(1054);
		WriteD(uint_0);
		if (uint_0 != 1)
		{
			return;
		}
		WriteH(0);
		WriteH((ushort)(list_0.Count + list_1.Count + list_2.Count + list_3.Count));
		foreach (ItemsModel item in list_0)
		{
			WriteD((uint)item.ObjectId);
			WriteD(item.Id);
			WriteC((byte)item.Equip);
			WriteD(item.Count);
		}
		foreach (ItemsModel item2 in list_1)
		{
			WriteD((uint)item2.ObjectId);
			WriteD(item2.Id);
			WriteC((byte)item2.Equip);
			WriteD(item2.Count);
		}
		foreach (ItemsModel item3 in list_2)
		{
			WriteD((uint)item3.ObjectId);
			WriteD(item3.Id);
			WriteC((byte)item3.Equip);
			WriteD(item3.Count);
		}
		foreach (ItemsModel item4 in list_3)
		{
			WriteD((uint)item4.ObjectId);
			WriteD(item4.Id);
			WriteC((byte)item4.Equip);
			WriteD(item4.Count);
		}
	}
}
