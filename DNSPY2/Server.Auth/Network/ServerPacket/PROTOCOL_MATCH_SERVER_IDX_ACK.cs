using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200000D RID: 13
	public class PROTOCOL_MATCH_SERVER_IDX_ACK : AuthServerPacket
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000255D File Offset: 0x0000075D
		public PROTOCOL_MATCH_SERVER_IDX_ACK(short short_1)
		{
			this.short_0 = short_1;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000256C File Offset: 0x0000076C
		public override void Write()
		{
			base.WriteH(7682);
			base.WriteH(0);
		}

		// Token: 0x04000021 RID: 33
		private readonly short short_0;
	}
}
