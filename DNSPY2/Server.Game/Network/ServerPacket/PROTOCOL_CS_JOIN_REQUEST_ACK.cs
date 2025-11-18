using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B6 RID: 182
	public class PROTOCOL_CS_JOIN_REQUEST_ACK : GameServerPacket
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public PROTOCOL_CS_JOIN_REQUEST_ACK(uint uint_1, int int_1)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00003DCD File Offset: 0x00001FCD
		public override void Write()
		{
			base.WriteH(813);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.int_0);
			}
		}

		// Token: 0x0400014E RID: 334
		private readonly int int_0;

		// Token: 0x0400014F RID: 335
		private readonly uint uint_0;
	}
}
