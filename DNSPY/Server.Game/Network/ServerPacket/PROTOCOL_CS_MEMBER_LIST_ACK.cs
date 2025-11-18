using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000BC RID: 188
	public class PROTOCOL_CS_MEMBER_LIST_ACK : GameServerPacket
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00003EEC File Offset: 0x000020EC
		public PROTOCOL_CS_MEMBER_LIST_ACK(uint uint_1, byte byte_3, byte byte_4, byte[] byte_5)
		{
			this.uint_0 = uint_1;
			this.byte_1 = byte_3;
			this.byte_2 = byte_4;
			this.byte_0 = byte_5;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00010560 File Offset: 0x0000E760
		public override void Write()
		{
			base.WriteH(805);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC(this.byte_1);
				base.WriteC(this.byte_2);
				base.WriteB(this.byte_0);
			}
		}

		// Token: 0x04000157 RID: 343
		private readonly uint uint_0;

		// Token: 0x04000158 RID: 344
		private readonly byte[] byte_0;

		// Token: 0x04000159 RID: 345
		private readonly byte byte_1;

		// Token: 0x0400015A RID: 346
		private readonly byte byte_2;
	}
}
