using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D7 RID: 215
	public class PROTOCOL_INVENTORY_GET_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0001096C File Offset: 0x0000EB6C
		public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<GoodsItem> list_1)
		{
			this.int_0 = int_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
			}
			foreach (GoodsItem goodsItem in list_1)
			{
				ItemsModel itemsModel = new ItemsModel(goodsItem.Item);
				if (itemsModel != null)
				{
					if (int_1 == 0)
					{
						ComDiv.TryCreateItem(itemsModel, account_0.Inventory, account_0.PlayerId);
					}
					SendItemInfo.LoadItem(account_0, itemsModel);
					this.list_0.Add(itemsModel);
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00010A10 File Offset: 0x0000EC10
		public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<ItemsModel> list_1)
		{
			this.int_0 = int_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
			}
			foreach (ItemsModel itemsModel in list_1)
			{
				ItemsModel itemsModel2 = new ItemsModel(itemsModel);
				if (itemsModel2 != null)
				{
					if (int_1 == 0)
					{
						ComDiv.TryCreateItem(itemsModel2, account_0.Inventory, account_0.PlayerId);
					}
					SendItemInfo.LoadItem(account_0, itemsModel2);
					this.list_0.Add(itemsModel2);
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, ItemsModel itemsModel_0)
		{
			this.int_0 = int_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
			}
			ItemsModel itemsModel = new ItemsModel(itemsModel_0);
			if (itemsModel != null)
			{
				if (int_1 == 0)
				{
					ComDiv.TryCreateItem(itemsModel, account_0.Inventory, account_0.PlayerId);
				}
				SendItemInfo.LoadItem(account_0, itemsModel);
				this.list_0.Add(itemsModel);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00010B18 File Offset: 0x0000ED18
		public override void Write()
		{
			base.WriteH(3334);
			base.WriteH(0);
			base.WriteH((ushort)this.playerInventory_0.Items.Count);
			base.WriteH((ushort)this.list_0.Count);
			foreach (ItemsModel itemsModel in this.list_0)
			{
				base.WriteD((uint)itemsModel.ObjectId);
				base.WriteD(itemsModel.Id);
				base.WriteC((byte)itemsModel.Equip);
				base.WriteD(itemsModel.Count);
			}
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x04000181 RID: 385
		private readonly int int_0;

		// Token: 0x04000182 RID: 386
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x04000183 RID: 387
		private readonly List<ItemsModel> list_0;
	}
}
