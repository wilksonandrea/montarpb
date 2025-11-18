using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000034 RID: 52
	public class PROTOCOL_AUTH_FRIEND_INSERT_ACK : GameServerPacket
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public PROTOCOL_AUTH_FRIEND_INSERT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public override void Write()
		{
			base.WriteH(1819);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000063 RID: 99
		private readonly uint uint_0;
	}
}
