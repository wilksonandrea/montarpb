using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000049 RID: 73
	public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK : GameServerPacket
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00002F4A File Offset: 0x0000114A
		public PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002F59 File Offset: 0x00001159
		public override void Write()
		{
			base.WriteH(1062);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000091 RID: 145
		private readonly uint uint_0;
	}
}
