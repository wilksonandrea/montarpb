using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200004F RID: 79
	public class PROTOCOL_BASE_GAMEGUARD_ACK : GameServerPacket
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00002FE8 File Offset: 0x000011E8
		public PROTOCOL_BASE_GAMEGUARD_ACK(int int_1, byte[] byte_1)
		{
			this.int_0 = int_1;
			this.byte_0 = byte_1;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002FFE File Offset: 0x000011FE
		public override void Write()
		{
			base.WriteH(2312);
			base.WriteB(this.byte_0);
			base.WriteD(this.int_0);
		}

		// Token: 0x040000A3 RID: 163
		private readonly int int_0;

		// Token: 0x040000A4 RID: 164
		private readonly byte[] byte_0;
	}
}
