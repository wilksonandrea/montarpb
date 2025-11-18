using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200003F RID: 63
	public class PROTOCOL_AUTH_SHOP_GOODSLIST_ACK : GameServerPacket
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00002E43 File Offset: 0x00001043
		public PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(ShopData shopData_1, int int_1)
		{
			this.shopData_0 = shopData_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public override void Write()
		{
			base.WriteH(1036);
			base.WriteD(this.int_0);
			base.WriteD(this.shopData_0.ItemsCount);
			base.WriteD(this.shopData_0.Offset);
			base.WriteB(this.shopData_0.Buffer);
			base.WriteD(50);
		}

		// Token: 0x0400007E RID: 126
		private readonly int int_0;

		// Token: 0x0400007F RID: 127
		private readonly ShopData shopData_0;
	}
}
