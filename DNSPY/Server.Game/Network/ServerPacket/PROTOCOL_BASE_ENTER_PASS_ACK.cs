using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200000A RID: 10
	public class PROTOCOL_BASE_ENTER_PASS_ACK : GameServerPacket
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002698 File Offset: 0x00000898
		public PROTOCOL_BASE_ENTER_PASS_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000026A7 File Offset: 0x000008A7
		public override void Write()
		{
			base.WriteH(2402);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400001E RID: 30
		private readonly uint uint_0;
	}
}
