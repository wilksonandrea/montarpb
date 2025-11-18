using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000017 RID: 23
	public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002805 File Offset: 0x00000A05
		public PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(uint uint_1, int int_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
			this.account_0 = account_1;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000099D4 File Offset: 0x00007BD4
		public override void Write()
		{
			base.WriteH(2361);
			base.WriteD(this.uint_0);
			base.WriteC((byte)this.int_0);
			if ((this.uint_0 & 1U) == 1U)
			{
				base.WriteD(this.account_0.Exp);
				base.WriteD(this.account_0.Gold);
				base.WriteD(this.account_0.Ribbon);
				base.WriteD(this.account_0.Ensign);
				base.WriteD(this.account_0.Medal);
				base.WriteD(this.account_0.MasterMedal);
				base.WriteD(this.account_0.Rank);
			}
		}

		// Token: 0x04000031 RID: 49
		private readonly Account account_0;

		// Token: 0x04000032 RID: 50
		private readonly uint uint_0;

		// Token: 0x04000033 RID: 51
		private readonly int int_0;
	}
}
