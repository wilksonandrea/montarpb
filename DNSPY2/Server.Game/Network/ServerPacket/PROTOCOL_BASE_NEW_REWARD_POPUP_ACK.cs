using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000058 RID: 88
	public class PROTOCOL_BASE_NEW_REWARD_POPUP_ACK : GameServerPacket
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x0000310F File Offset: 0x0000130F
		public PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Account account_0, ItemsModel itemsModel_1)
		{
			this.itemsModel_0 = itemsModel_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
				if (itemsModel_1 != null)
				{
					this.list_0.Add(itemsModel_1);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public override void Write()
		{
			base.WriteH(2430);
			base.WriteH(0);
			base.WriteH((ushort)this.playerInventory_0.Items.Count);
			base.WriteC(1);
			base.WriteD(this.itemsModel_0.Id);
		}

		// Token: 0x040000AE RID: 174
		private readonly ItemsModel itemsModel_0;

		// Token: 0x040000AF RID: 175
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x040000B0 RID: 176
		private readonly List<ItemsModel> list_0;
	}
}
