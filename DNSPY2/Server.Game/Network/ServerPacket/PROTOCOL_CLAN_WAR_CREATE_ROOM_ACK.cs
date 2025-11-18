using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200008A RID: 138
	public class PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK : GameServerPacket
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000374F File Offset: 0x0000194F
		public PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(MatchModel matchModel_0)
		{
			this.match = matchModel_0;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000EA74 File Offset: 0x0000CC74
		public override void Write()
		{
			base.WriteH(1564);
			base.WriteH((short)this.match.MatchId);
			base.WriteD(this.match.GetServerInfo());
			base.WriteH((short)this.match.GetServerInfo());
			base.WriteC(10);
		}

		// Token: 0x0400010B RID: 267
		public readonly MatchModel match;
	}
}
