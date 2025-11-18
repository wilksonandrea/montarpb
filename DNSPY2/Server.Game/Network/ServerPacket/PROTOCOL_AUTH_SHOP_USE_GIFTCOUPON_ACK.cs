using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000048 RID: 72
	public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK : GameServerPacket
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00002F1B File Offset: 0x0000111B
		public PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002F2A File Offset: 0x0000112A
		public override void Write()
		{
			base.WriteH(1085);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000090 RID: 144
		private readonly uint uint_0;
	}
}
