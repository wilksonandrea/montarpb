using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C7 RID: 199
	public class PROTOCOL_CS_REPLACE_NAME_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000408C File Offset: 0x0000228C
		public PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000409B File Offset: 0x0000229B
		public override void Write()
		{
			base.WriteH(864);
			base.WriteU(this.string_0, 34);
		}

		// Token: 0x04000169 RID: 361
		private readonly string string_0;
	}
}
