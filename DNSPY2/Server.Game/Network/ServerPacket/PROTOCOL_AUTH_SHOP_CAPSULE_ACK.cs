using System;
using System.Collections.Generic;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200003C RID: 60
	public class PROTOCOL_AUTH_SHOP_CAPSULE_ACK : GameServerPacket
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(ItemsModel itemsModel_0, int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.list_0 = new List<ItemsModel>();
			ItemsModel itemsModel = new ItemsModel(itemsModel_0);
			if (itemsModel != null)
			{
				this.list_0.Add(itemsModel);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(List<ItemsModel> list_1, int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.list_0 = new List<ItemsModel>();
			foreach (ItemsModel itemsModel in list_1)
			{
				ItemsModel itemsModel2 = new ItemsModel(itemsModel);
				if (itemsModel2 != null)
				{
					this.list_0.Add(itemsModel2);
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public override void Write()
		{
			base.WriteH(1064);
			base.WriteH(0);
			base.WriteC(1);
			base.WriteC((byte)this.int_1);
			base.WriteC((byte)this.list_0.Count);
			foreach (ItemsModel itemsModel in this.list_0)
			{
				base.WriteD(itemsModel.Id);
			}
		}

		// Token: 0x04000078 RID: 120
		private readonly List<ItemsModel> list_0;

		// Token: 0x04000079 RID: 121
		private readonly int int_0;

		// Token: 0x0400007A RID: 122
		private readonly int int_1;
	}
}
