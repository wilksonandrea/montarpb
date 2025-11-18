using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200002B RID: 43
	public class PROTOCOL_BASE_USER_GIFTLIST_ACK : AuthServerPacket
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002895 File Offset: 0x00000A95
		public PROTOCOL_BASE_USER_GIFTLIST_ACK(int int_1, List<MessageModel> list_1)
		{
			this.int_0 = int_1;
			this.list_0 = list_1;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006C30 File Offset: 0x00004E30
		public override void Write()
		{
			base.WriteH(1042);
			base.WriteC(0);
			base.WriteC((byte)this.list_0.Count);
			for (int i = 0; i < this.list_0.Count; i++)
			{
				MessageModel messageModel = this.list_0[i];
			}
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}

		// Token: 0x0400005A RID: 90
		private readonly int int_0;

		// Token: 0x0400005B RID: 91
		private readonly List<MessageModel> list_0;
	}
}
