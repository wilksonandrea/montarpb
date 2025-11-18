using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200003B RID: 59
	public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK : GameServerPacket
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000A890 File Offset: 0x00008A90
		public PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(uint uint_1, ItemsModel itemsModel_0 = null, Account account_0 = null)
		{
			this.uint_0 = uint_1;
			ItemsModel itemsModel = new ItemsModel(itemsModel_0);
			if (itemsModel != null)
			{
				ComDiv.TryCreateItem(itemsModel, account_0.Inventory, account_0.PlayerId);
				if (itemsModel.Category == ItemCategory.Weapon)
				{
					this.list_1.Add(itemsModel);
					return;
				}
				if (itemsModel.Category == ItemCategory.Character)
				{
					this.list_0.Add(itemsModel);
					return;
				}
				if (itemsModel.Category == ItemCategory.Coupon)
				{
					this.list_2.Add(itemsModel);
					return;
				}
				if (itemsModel.Category == ItemCategory.NewItem)
				{
					this.list_3.Add(itemsModel);
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000A94C File Offset: 0x00008B4C
		public override void Write()
		{
			base.WriteH(1054);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 1U)
			{
				base.WriteH(0);
				base.WriteH((ushort)(this.list_0.Count + this.list_1.Count + this.list_2.Count + this.list_3.Count));
				foreach (ItemsModel itemsModel in this.list_0)
				{
					base.WriteD((uint)itemsModel.ObjectId);
					base.WriteD(itemsModel.Id);
					base.WriteC((byte)itemsModel.Equip);
					base.WriteD(itemsModel.Count);
				}
				foreach (ItemsModel itemsModel2 in this.list_1)
				{
					base.WriteD((uint)itemsModel2.ObjectId);
					base.WriteD(itemsModel2.Id);
					base.WriteC((byte)itemsModel2.Equip);
					base.WriteD(itemsModel2.Count);
				}
				foreach (ItemsModel itemsModel3 in this.list_2)
				{
					base.WriteD((uint)itemsModel3.ObjectId);
					base.WriteD(itemsModel3.Id);
					base.WriteC((byte)itemsModel3.Equip);
					base.WriteD(itemsModel3.Count);
				}
				foreach (ItemsModel itemsModel4 in this.list_3)
				{
					base.WriteD((uint)itemsModel4.ObjectId);
					base.WriteD(itemsModel4.Id);
					base.WriteC((byte)itemsModel4.Equip);
					base.WriteD(itemsModel4.Count);
				}
			}
		}

		// Token: 0x04000073 RID: 115
		private readonly uint uint_0;

		// Token: 0x04000074 RID: 116
		private readonly List<ItemsModel> list_0 = new List<ItemsModel>();

		// Token: 0x04000075 RID: 117
		private readonly List<ItemsModel> list_1 = new List<ItemsModel>();

		// Token: 0x04000076 RID: 118
		private readonly List<ItemsModel> list_2 = new List<ItemsModel>();

		// Token: 0x04000077 RID: 119
		private readonly List<ItemsModel> list_3 = new List<ItemsModel>();
	}
}
