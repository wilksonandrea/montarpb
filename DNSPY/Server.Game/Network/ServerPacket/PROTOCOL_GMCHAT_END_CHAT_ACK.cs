using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D0 RID: 208
	public class PROTOCOL_GMCHAT_END_CHAT_ACK : GameServerPacket
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000420D File Offset: 0x0000240D
		public PROTOCOL_GMCHAT_END_CHAT_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00004223 File Offset: 0x00002423
		public override void Write()
		{
			base.WriteH(6662);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000178 RID: 376
		private readonly Account account_0;

		// Token: 0x04000179 RID: 377
		private readonly uint uint_0;
	}
}
