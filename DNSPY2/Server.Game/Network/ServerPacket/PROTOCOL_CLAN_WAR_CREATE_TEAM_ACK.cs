using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200008B RID: 139
	public class PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK : GameServerPacket
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000375E File Offset: 0x0000195E
		public PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(uint uint_1, MatchModel matchModel_1 = null)
		{
			this.uint_0 = uint_1;
			this.matchModel_0 = matchModel_1;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public override void Write()
		{
			base.WriteH(6919);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteH((short)this.matchModel_0.MatchId);
				base.WriteH((short)this.matchModel_0.GetServerInfo());
				base.WriteH((short)this.matchModel_0.GetServerInfo());
				base.WriteC((byte)this.matchModel_0.State);
				base.WriteC((byte)this.matchModel_0.FriendId);
				base.WriteC((byte)this.matchModel_0.Training);
				base.WriteC((byte)this.matchModel_0.GetCountPlayers());
				base.WriteD(this.matchModel_0.Leader);
				base.WriteC(0);
			}
		}

		// Token: 0x0400010C RID: 268
		private readonly uint uint_0;

		// Token: 0x0400010D RID: 269
		private readonly MatchModel matchModel_0;
	}
}
