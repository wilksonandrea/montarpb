using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A0 RID: 160
	public class PROTOCOL_CS_CHECK_MARK_ACK : GameServerPacket
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public PROTOCOL_CS_CHECK_MARK_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public override void Write()
		{
			base.WriteH(857);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000132 RID: 306
		private readonly uint uint_0;
	}
}
