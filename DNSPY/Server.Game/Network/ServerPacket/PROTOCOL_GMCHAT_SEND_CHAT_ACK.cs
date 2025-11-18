using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000013 RID: 19
	public class PROTOCOL_GMCHAT_SEND_CHAT_ACK : GameServerPacket
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002775 File Offset: 0x00000975
		public PROTOCOL_GMCHAT_SEND_CHAT_ACK(string string_3, string string_4, string string_5, Account account_1)
		{
			this.string_0 = string_3;
			this.string_2 = string_4;
			this.string_1 = string_5;
			this.account_0 = account_1;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00009674 File Offset: 0x00007874
		public override void Write()
		{
			base.WriteH(6660);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteH((short)((byte)this.string_2.Length));
			base.WriteU(this.string_2, this.string_2.Length * 2);
			base.WriteC((byte)this.string_1.Length);
			base.WriteU(this.string_1, this.string_1.Length * 2);
		}

		// Token: 0x04000028 RID: 40
		private readonly Account account_0;

		// Token: 0x04000029 RID: 41
		private readonly string string_0;

		// Token: 0x0400002A RID: 42
		private readonly string string_1;

		// Token: 0x0400002B RID: 43
		private readonly string string_2;
	}
}
