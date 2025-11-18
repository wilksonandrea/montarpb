using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_INVENTORY_GET_INFO_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly PlayerInventory playerInventory_0;

		private readonly List<ItemsModel> list_0;

		public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<GoodsItem> list_1)
		{
			this.int_0 = int_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
			}
			foreach (GoodsItem list1 in list_1)
			{
				ItemsModel ıtemsModel = new ItemsModel(list1.Item);
				if (ıtemsModel == null)
				{
					continue;
				}
				if (int_1 == 0)
				{
					ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
				}
				SendItemInfo.LoadItem(account_0, ıtemsModel);
				this.list_0.Add(ıtemsModel);
			}
		}

		public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<ItemsModel> list_1)
		{
			this.int_0 = int_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
			}
			foreach (ItemsModel list1 in list_1)
			{
				ItemsModel ıtemsModel = new ItemsModel(list1);
				if (ıtemsModel == null)
				{
					continue;
				}
				if (int_1 == 0)
				{
					ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
				}
				SendItemInfo.LoadItem(account_0, ıtemsModel);
				this.list_0.Add(ıtemsModel);
			}
		}

		public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, ItemsModel itemsModel_0)
		{
			this.int_0 = int_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
			}
			ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
			if (ıtemsModel != null)
			{
				if (int_1 == 0)
				{
					ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
				}
				SendItemInfo.LoadItem(account_0, ıtemsModel);
				this.list_0.Add(ıtemsModel);
			}
		}

		public override void Write()
		{
			base.WriteH(3334);
			base.WriteH(0);
			base.WriteH((ushort)this.playerInventory_0.Items.Count);
			base.WriteH((ushort)this.list_0.Count);
			foreach (ItemsModel list0 in this.list_0)
			{
				base.WriteD((uint)list0.ObjectId);
				base.WriteD(list0.Id);
				base.WriteC((byte)list0.Equip);
				base.WriteD(list0.Count);
			}
			base.WriteC((byte)this.int_0);
		}
	}
}