using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000016 RID: 22
	public class PROTOCOL_BASE_MEDAL_GET_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000027F6 File Offset: 0x000009F6
		public PROTOCOL_BASE_MEDAL_GET_INFO_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00009964 File Offset: 0x00007B64
		public override void Write()
		{
			base.WriteH(2363);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteD(this.account_0.Ribbon);
			base.WriteD(this.account_0.Ensign);
			base.WriteD(this.account_0.Medal);
			base.WriteD(this.account_0.MasterMedal);
		}

		// Token: 0x04000030 RID: 48
		private readonly Account account_0;
	}
}
