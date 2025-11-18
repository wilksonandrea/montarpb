using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000041 RID: 65
	public class PROTOCOL_AUTH_SHOP_ITEMLIST_ACK : GameServerPacket
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00002E85 File Offset: 0x00001085
		public PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(ShopData shopData_1, int int_1)
		{
			this.shopData_0 = shopData_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000AE0C File Offset: 0x0000900C
		public override void Write()
		{
			base.WriteH(1038);
			base.WriteD(this.int_0);
			base.WriteD(this.shopData_0.ItemsCount);
			base.WriteD(this.shopData_0.Offset);
			base.WriteB(this.shopData_0.Buffer);
			base.WriteD(800);
		}

		// Token: 0x04000083 RID: 131
		private readonly int int_0;

		// Token: 0x04000084 RID: 132
		private readonly ShopData shopData_0;
	}
}
