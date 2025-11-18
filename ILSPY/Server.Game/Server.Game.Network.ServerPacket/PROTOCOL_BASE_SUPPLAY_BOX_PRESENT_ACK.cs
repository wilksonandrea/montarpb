using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly List<ItemsModel> list_0 = new List<ItemsModel>();

	private readonly List<ItemsModel> list_1 = new List<ItemsModel>();

	private readonly List<ItemsModel> list_2 = new List<ItemsModel>();

	public PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK(uint uint_1, ItemsModel itemsModel_0, Account account_0)
	{
		uint_0 = uint_1;
		ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
		if (ıtemsModel != null)
		{
			ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
			SendItemInfo.LoadItem(account_0, ıtemsModel);
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
		}
	}

	public override void Write()
	{
		WriteH(2411);
		WriteD(list_0.Count);
		WriteD(list_1.Count);
		WriteD(list_2.Count);
		WriteD(0);
		foreach (ItemsModel item in list_0)
		{
			WriteQ(item.ObjectId);
			WriteD(item.Id);
			WriteC((byte)item.Equip);
			WriteD(item.Count);
		}
		foreach (ItemsModel item2 in list_1)
		{
			WriteQ(item2.ObjectId);
			WriteD(item2.Id);
			WriteC((byte)item2.Equip);
			WriteD(item2.Count);
		}
		foreach (ItemsModel item3 in list_2)
		{
			WriteQ(item3.ObjectId);
			WriteD(item3.Id);
			WriteC((byte)item3.Equip);
			WriteD(item3.Count);
		}
	}
}
