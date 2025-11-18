using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000BE RID: 190
	public class PROTOCOL_CS_PAGE_CHATTING_ACK : GameServerPacket
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00003F39 File Offset: 0x00002139
		public PROTOCOL_CS_PAGE_CHATTING_ACK(Account account_0, string string_2)
		{
			this.string_0 = account_0.Nickname;
			this.string_1 = string_2;
			this.bool_0 = account_0.UseChatGM();
			this.int_2 = account_0.NickColor;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00003F6C File Offset: 0x0000216C
		public PROTOCOL_CS_PAGE_CHATTING_ACK(int int_3, int int_4)
		{
			this.int_0 = int_3;
			this.int_1 = int_4;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public override void Write()
		{
			base.WriteH(887);
			base.WriteC((byte)this.int_0);
			if (this.int_0 == 0)
			{
				base.WriteC((byte)(this.string_0.Length + 1));
				base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
				base.WriteC((this.bool_0 > false) ? 1 : 0);
				base.WriteC((byte)this.int_2);
				base.WriteC((byte)(this.string_1.Length + 1));
				base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
				return;
			}
			base.WriteD(this.int_1);
		}

		// Token: 0x0400015C RID: 348
		private readonly string string_0;

		// Token: 0x0400015D RID: 349
		private readonly string string_1;

		// Token: 0x0400015E RID: 350
		private readonly int int_0;

		// Token: 0x0400015F RID: 351
		private readonly int int_1;

		// Token: 0x04000160 RID: 352
		private readonly int int_2;

		// Token: 0x04000161 RID: 353
		private readonly bool bool_0;
	}
}
