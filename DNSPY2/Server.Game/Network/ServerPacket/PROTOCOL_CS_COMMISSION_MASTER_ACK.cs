using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A8 RID: 168
	public class PROTOCOL_CS_COMMISSION_MASTER_ACK : GameServerPacket
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00003BED File Offset: 0x00001DED
		public PROTOCOL_CS_COMMISSION_MASTER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00003BFC File Offset: 0x00001DFC
		public override void Write()
		{
			base.WriteH(834);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000140 RID: 320
		private readonly uint uint_0;
	}
}
