using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_INVENTORY_GET_INFO_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly List<ItemsModel> list_0;

	public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<GoodsItem> list_1)
	{
		int_0 = int_1;
		if (account_0 != null)
		{
			playerInventory_0 = account_0.Inventory;
			list_0 = new List<ItemsModel>();
		}
		foreach (GoodsItem item in list_1)
		{
			ItemsModel ıtemsModel = new ItemsModel(item.Item);
			if (ıtemsModel != null)
			{
				if (int_1 == 0)
				{
					ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
				}
				SendItemInfo.LoadItem(account_0, ıtemsModel);
				list_0.Add(ıtemsModel);
			}
		}
	}

	public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<ItemsModel> list_1)
	{
		int_0 = int_1;
		if (account_0 != null)
		{
			playerInventory_0 = account_0.Inventory;
			list_0 = new List<ItemsModel>();
		}
		foreach (ItemsModel item in list_1)
		{
			ItemsModel ıtemsModel = new ItemsModel(item);
			if (ıtemsModel != null)
			{
				if (int_1 == 0)
				{
					ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
				}
				SendItemInfo.LoadItem(account_0, ıtemsModel);
				list_0.Add(ıtemsModel);
			}
		}
	}

	public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, ItemsModel itemsModel_0)
	{
		int_0 = int_1;
		if (account_0 != null)
		{
			playerInventory_0 = account_0.Inventory;
			list_0 = new List<ItemsModel>();
		}
		ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
		if (ıtemsModel != null)
		{
			if (int_1 == 0)
			{
				ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
			}
			SendItemInfo.LoadItem(account_0, ıtemsModel);
			list_0.Add(ıtemsModel);
		}
	}

	public override void Write()
	{
		WriteH(3334);
		WriteH(0);
		WriteH((ushort)playerInventory_0.Items.Count);
		WriteH((ushort)list_0.Count);
		foreach (ItemsModel item in list_0)
		{
			WriteD((uint)item.ObjectId);
			WriteD(item.Id);
			WriteC((byte)item.Equip);
			WriteD(item.Count);
		}
		WriteC((byte)int_0);
	}
}
