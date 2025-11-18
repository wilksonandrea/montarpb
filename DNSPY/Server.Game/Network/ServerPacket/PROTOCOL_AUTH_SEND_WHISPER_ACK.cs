using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200003A RID: 58
	public class PROTOCOL_AUTH_SEND_WHISPER_ACK : GameServerPacket
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00002DBA File Offset: 0x00000FBA
		public PROTOCOL_AUTH_SEND_WHISPER_ACK(string string_2, string string_3, uint uint_1)
		{
			this.string_0 = string_2;
			this.string_1 = string_3;
			this.uint_0 = uint_1;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000A814 File Offset: 0x00008A14
		public override void Write()
		{
			base.WriteH(1827);
			base.WriteD(this.uint_0);
			base.WriteC((byte)this.int_0);
			base.WriteU(this.string_0, 66);
			if (this.uint_0 == 0U)
			{
				base.WriteH((ushort)(this.string_1.Length + 1));
				base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
			}
		}

		// Token: 0x0400006E RID: 110
		private readonly string string_0;

		// Token: 0x0400006F RID: 111
		private readonly string string_1;

		// Token: 0x04000070 RID: 112
		private readonly uint uint_0;

		// Token: 0x04000071 RID: 113
		private readonly int int_0;

		// Token: 0x04000072 RID: 114
		private readonly int int_1;
	}
}
