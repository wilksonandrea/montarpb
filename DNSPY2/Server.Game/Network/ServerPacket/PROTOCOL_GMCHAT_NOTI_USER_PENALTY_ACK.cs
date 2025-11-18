using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000012 RID: 18
	public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK : GameServerPacket
	{
		// Token: 0x06000060 RID: 96 RVA: 0x0000275F File Offset: 0x0000095F
		public PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000095EC File Offset: 0x000077EC
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

		// Token: 0x04000026 RID: 38
		private readonly Account account_0;

		// Token: 0x04000027 RID: 39
		private readonly uint uint_0;
	}
}
