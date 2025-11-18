using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200003E RID: 62
	public class PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK : GameServerPacket
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00002E20 File Offset: 0x00001020
		public PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002E2F File Offset: 0x0000102F
		public override void Write()
		{
			base.WriteH(1042);
			base.WriteH(0);
		}

		// Token: 0x0400007D RID: 125
		private readonly uint uint_0;
	}
}
