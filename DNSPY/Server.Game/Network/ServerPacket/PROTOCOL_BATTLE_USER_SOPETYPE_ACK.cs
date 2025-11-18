using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000082 RID: 130
	public class PROTOCOL_BATTLE_USER_SOPETYPE_ACK : GameServerPacket
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000362A File Offset: 0x0000182A
		public PROTOCOL_BATTLE_USER_SOPETYPE_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003639 File Offset: 0x00001839
		public override void Write()
		{
			base.WriteH(5277);
			base.WriteD(this.account_0.SlotId);
			base.WriteC((byte)this.account_0.Sight);
		}

		// Token: 0x040000FE RID: 254
		private readonly Account account_0;
	}
}
