using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000AA RID: 170
	public class PROTOCOL_CS_COMMISSION_REGULAR_ACK : GameServerPacket
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00003C22 File Offset: 0x00001E22
		public PROTOCOL_CS_COMMISSION_REGULAR_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00003C31 File Offset: 0x00001E31
		public override void Write()
		{
			base.WriteH(840);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000141 RID: 321
		private readonly uint uint_0;
	}
}
