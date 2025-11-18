using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200008F RID: 143
	public class PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK : GameServerPacket
	{
		// Token: 0x06000176 RID: 374 RVA: 0x000037EF File Offset: 0x000019EF
		public PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(MatchModel matchModel_1, int int_2, int int_3)
		{
			this.matchModel_0 = matchModel_1;
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000380C File Offset: 0x00001A0C
		public override void Write()
		{
			base.WriteH(1566);
			base.WriteD(this.int_0);
			base.WriteH((ushort)this.int_1);
			base.WriteH((ushort)this.matchModel_0.GetServerInfo());
		}

		// Token: 0x04000112 RID: 274
		private readonly MatchModel matchModel_0;

		// Token: 0x04000113 RID: 275
		private readonly int int_0;

		// Token: 0x04000114 RID: 276
		private readonly int int_1;
	}
}
