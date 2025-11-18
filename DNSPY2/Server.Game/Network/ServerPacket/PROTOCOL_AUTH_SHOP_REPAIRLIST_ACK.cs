using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000047 RID: 71
	public class PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK : GameServerPacket
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00002F05 File Offset: 0x00001105
		public PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(ShopData shopData_1, int int_1)
		{
			this.shopData_0 = shopData_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000B19C File Offset: 0x0000939C
		public override void Write()
		{
			base.WriteH(1070);
			base.WriteD(this.int_0);
			base.WriteD(this.shopData_0.ItemsCount);
			base.WriteD(this.shopData_0.Offset);
			base.WriteB(this.shopData_0.Buffer);
			base.WriteD(100);
		}

		// Token: 0x0400008E RID: 142
		private readonly int int_0;

		// Token: 0x0400008F RID: 143
		private readonly ShopData shopData_0;
	}
}
