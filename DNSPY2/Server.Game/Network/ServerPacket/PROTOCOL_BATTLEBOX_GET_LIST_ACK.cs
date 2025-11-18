using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200005E RID: 94
	public class PROTOCOL_BATTLEBOX_GET_LIST_ACK : GameServerPacket
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00003257 File Offset: 0x00001457
		public PROTOCOL_BATTLEBOX_GET_LIST_ACK(ShopData shopData_1, int int_1)
		{
			this.shopData_0 = shopData_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000C034 File Offset: 0x0000A234
		public override void Write()
		{
			base.WriteH(7426);
			base.WriteD(this.int_0);
			base.WriteD(this.shopData_0.ItemsCount);
			base.WriteD(this.shopData_0.Offset);
			base.WriteB(this.shopData_0.Buffer);
			base.WriteD(585);
		}

		// Token: 0x040000B8 RID: 184
		private readonly int int_0;

		// Token: 0x040000B9 RID: 185
		private readonly ShopData shopData_0;
	}
}
