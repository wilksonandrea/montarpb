using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000BA RID: 186
	public class PROTOCOL_CS_MEMBER_INFO_DELETE_ACK : GameServerPacket
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00003EB5 File Offset: 0x000020B5
		public PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(long long_1)
		{
			this.long_0 = long_1;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00003EC4 File Offset: 0x000020C4
		public override void Write()
		{
			base.WriteH(849);
			base.WriteQ(this.long_0);
		}

		// Token: 0x04000155 RID: 341
		private readonly long long_0;
	}
}
