using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E2 RID: 226
	public class PROTOCOL_MATCH_SERVER_IDX_ACK : GameServerPacket
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00004478 File Offset: 0x00002678
		public PROTOCOL_MATCH_SERVER_IDX_ACK(short short_1)
		{
			this.short_0 = short_1;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00004487 File Offset: 0x00002687
		public override void Write()
		{
			base.WriteH(7682);
			base.WriteH(0);
		}

		// Token: 0x040001A5 RID: 421
		private readonly short short_0;
	}
}
