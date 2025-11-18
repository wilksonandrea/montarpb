using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200002D RID: 45
	public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : GameServerPacket
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00002B96 File Offset: 0x00000D96
		public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public override void Write()
		{
			base.WriteH(1989);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x04000059 RID: 89
		private readonly int int_0;
	}
}
