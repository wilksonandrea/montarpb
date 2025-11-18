using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200001F RID: 31
	public class PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK : GameServerPacket
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00009FC4 File Offset: 0x000081C4
		public PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK(uint uint_1, ItemsModel itemsModel_0, Account account_0)
		{
			this.uint_0 = uint_1;
			ItemsModel itemsModel = new ItemsModel(itemsModel_0);
			if (itemsModel != null)
			{
				ComDiv.TryCreateItem(itemsModel, account_0.Inventory, account_0.PlayerId);
				SendItemInfo.LoadItem(account_0, itemsModel);
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
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000A064 File Offset: 0x00008264
		public override void Write()
		{
			base.WriteH(2411);
			base.WriteD(this.list_0.Count);
			base.WriteD(this.list_1.Count);
			base.WriteD(this.list_2.Count);
			base.WriteD(0);
			foreach (ItemsModel itemsModel in this.list_0)
			{
				base.WriteQ(itemsModel.ObjectId);
				base.WriteD(itemsModel.Id);
				base.WriteC((byte)itemsModel.Equip);
				base.WriteD(itemsModel.Count);
			}
			foreach (ItemsModel itemsModel2 in this.list_1)
			{
				base.WriteQ(itemsModel2.ObjectId);
				base.WriteD(itemsModel2.Id);
				base.WriteC((byte)itemsModel2.Equip);
				base.WriteD(itemsModel2.Count);
			}
			foreach (ItemsModel itemsModel3 in this.list_2)
			{
				base.WriteQ(itemsModel3.ObjectId);
				base.WriteD(itemsModel3.Id);
				base.WriteC((byte)itemsModel3.Equip);
				base.WriteD(itemsModel3.Count);
			}
		}

		// Token: 0x0400003F RID: 63
		private readonly uint uint_0;

		// Token: 0x04000040 RID: 64
		private readonly List<ItemsModel> list_0 = new List<ItemsModel>();

		// Token: 0x04000041 RID: 65
		private readonly List<ItemsModel> list_1 = new List<ItemsModel>();

		// Token: 0x04000042 RID: 66
		private readonly List<ItemsModel> list_2 = new List<ItemsModel>();
	}
}
