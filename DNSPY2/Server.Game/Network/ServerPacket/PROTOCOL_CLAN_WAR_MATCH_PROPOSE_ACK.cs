using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000092 RID: 146
	public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK : GameServerPacket
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00003882 File Offset: 0x00001A82
		public PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003891 File Offset: 0x00001A91
		public override void Write()
		{
			base.WriteH(1554);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000118 RID: 280
		private readonly uint uint_0;
	}
}
