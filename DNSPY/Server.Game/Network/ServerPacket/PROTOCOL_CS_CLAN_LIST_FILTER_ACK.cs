using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A4 RID: 164
	public class PROTOCOL_CS_CLAN_LIST_FILTER_ACK : GameServerPacket
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00003B7E File Offset: 0x00001D7E
		public PROTOCOL_CS_CLAN_LIST_FILTER_ACK(byte byte_2, int int_1, byte[] byte_3)
		{
			this.byte_0 = byte_2;
			this.int_0 = int_1;
			this.byte_1 = byte_3;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F948 File Offset: 0x0000DB48
		public override void Write()
		{
			base.WriteH(998);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteC(this.byte_0);
			base.WriteH((ushort)this.int_0);
			base.WriteD(this.int_0);
			base.WriteB(this.byte_1);
		}

		// Token: 0x0400013A RID: 314
		private readonly byte byte_0;

		// Token: 0x0400013B RID: 315
		private readonly int int_0;

		// Token: 0x0400013C RID: 316
		private readonly byte[] byte_1;
	}
}
