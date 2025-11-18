using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C3 RID: 195
	public class PROTOCOL_CS_REPLACE_MANAGEMENT_ACK : GameServerPacket
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00003FED File Offset: 0x000021ED
		public PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00003FFC File Offset: 0x000021FC
		public override void Write()
		{
			base.WriteH(869);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000164 RID: 356
		private readonly uint uint_0;
	}
}
