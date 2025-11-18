using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000063 RID: 99
	public class PROTOCOL_BATTLE_GIVEUPBATTLE_ACK : GameServerPacket
	{
		// Token: 0x0600010F RID: 271 RVA: 0x000032C2 File Offset: 0x000014C2
		public PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Account account_1, int int_1)
		{
			this.account_0 = account_1;
			this.int_0 = int_1;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public override void Write()
		{
			base.WriteH(5134);
			base.WriteD(this.account_0.SlotId);
			base.WriteC((byte)this.int_0);
			base.WriteD(this.account_0.Exp);
			base.WriteD(this.account_0.Rank);
			base.WriteD(this.account_0.Gold);
			base.WriteD(this.account_0.Statistic.Season.EscapesCount);
			base.WriteD(this.account_0.Statistic.Basic.EscapesCount);
			base.WriteD(0);
			base.WriteC(0);
		}

		// Token: 0x040000C7 RID: 199
		private readonly Account account_0;

		// Token: 0x040000C8 RID: 200
		private readonly int int_0;
	}
}
