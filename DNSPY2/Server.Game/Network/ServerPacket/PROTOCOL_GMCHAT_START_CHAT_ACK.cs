using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D1 RID: 209
	public class PROTOCOL_GMCHAT_START_CHAT_ACK : GameServerPacket
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00004243 File Offset: 0x00002443
		public PROTOCOL_GMCHAT_START_CHAT_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000108E4 File Offset: 0x0000EAE4
		public override void Write()
		{
			base.WriteH(6658);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC((byte)(this.account_0.Nickname.Length + 1));
				base.WriteN(this.account_0.Nickname, this.account_0.Nickname.Length + 2, "UTF-16LE");
				base.WriteQ(this.account_0.PlayerId);
			}
		}

		// Token: 0x0400017A RID: 378
		private readonly Account account_0;

		// Token: 0x0400017B RID: 379
		private readonly uint uint_0;
	}
}
