using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200009D RID: 157
	public class PROTOCOL_CS_CHATTING_ACK : GameServerPacket
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00003A39 File Offset: 0x00001C39
		public PROTOCOL_CS_CHATTING_ACK(string string_1, Account account_1)
		{
			this.string_0 = string_1;
			this.account_0 = account_1;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003A4F File Offset: 0x00001C4F
		public PROTOCOL_CS_CHATTING_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public override void Write()
		{
			base.WriteH(855);
			if (this.int_0 == 0)
			{
				base.WriteC((byte)(this.account_0.Nickname.Length + 1));
				base.WriteN(this.account_0.Nickname, this.account_0.Nickname.Length + 2, "UTF-16LE");
				base.WriteC((this.account_0.UseChatGM() > false) ? 1 : 0);
				base.WriteC((byte)this.account_0.NickColor);
				base.WriteC((byte)(this.string_0.Length + 1));
				base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
				return;
			}
			base.WriteD(this.int_1);
		}

		// Token: 0x0400012C RID: 300
		private readonly string string_0;

		// Token: 0x0400012D RID: 301
		private readonly Account account_0;

		// Token: 0x0400012E RID: 302
		private readonly int int_0;

		// Token: 0x0400012F RID: 303
		private readonly int int_1;
	}
}
