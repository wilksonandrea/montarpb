using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200011E RID: 286
	public class PROTOCOL_SHOP_REPAIR_ACK : GameServerPacket
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00004C69 File Offset: 0x00002E69
		public PROTOCOL_SHOP_REPAIR_ACK(uint uint_1, List<ItemsModel> list_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			this.list_0 = list_1;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000145FC File Offset: 0x000127FC
		public override void Write()
		{
			base.WriteH(1077);
			base.WriteH(0);
			if (this.uint_0 == 1U)
			{
				base.WriteC((byte)this.list_0.Count);
				foreach (ItemsModel itemsModel in this.list_0)
				{
					base.WriteD((uint)itemsModel.ObjectId);
					base.WriteD(itemsModel.Id);
				}
				base.WriteD(this.account_0.Cash);
				base.WriteD(this.account_0.Gold);
				return;
			}
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000204 RID: 516
		private readonly uint uint_0;

		// Token: 0x04000205 RID: 517
		private readonly List<ItemsModel> list_0;

		// Token: 0x04000206 RID: 518
		private readonly Account account_0;
	}
}
