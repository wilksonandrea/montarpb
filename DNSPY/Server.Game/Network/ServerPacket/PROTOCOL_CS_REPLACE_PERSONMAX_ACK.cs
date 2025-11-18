using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C9 RID: 201
	public class PROTOCOL_CS_REPLACE_PERSONMAX_ACK : GameServerPacket
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x000040DE File Offset: 0x000022DE
		public PROTOCOL_CS_REPLACE_PERSONMAX_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000040ED File Offset: 0x000022ED
		public override void Write()
		{
			base.WriteH(873);
			base.WriteD(0);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x0400016B RID: 363
		private readonly int int_0;
	}
}
