using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000039 RID: 57
	public class PROTOCOL_AUTH_RECV_WHISPER_ACK : GameServerPacket
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002D9D File Offset: 0x00000F9D
		public PROTOCOL_AUTH_RECV_WHISPER_ACK(string string_2, string string_3, bool bool_1)
		{
			this.string_0 = string_2;
			this.string_1 = string_3;
			this.bool_0 = bool_1;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000A79C File Offset: 0x0000899C
		public override void Write()
		{
			base.WriteH(1830);
			base.WriteU(this.string_0, 66);
			base.WriteC((this.bool_0 > false) ? 1 : 0);
			base.WriteC(0);
			base.WriteH((ushort)(this.string_1.Length + 1));
			base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
			base.WriteC(0);
		}

		// Token: 0x0400006B RID: 107
		private readonly string string_0;

		// Token: 0x0400006C RID: 108
		private readonly string string_1;

		// Token: 0x0400006D RID: 109
		private readonly bool bool_0;
	}
}
