using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000093 RID: 147
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK : GameServerPacket
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000038AA File Offset: 0x00001AAA
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000038B9 File Offset: 0x00001AB9
		public override void Write()
		{
			base.WriteH(6915);
			base.WriteH((short)this.int_0);
			base.WriteC(13);
			base.WriteH((short)Math.Ceiling((double)this.int_0 / 13.0));
		}

		// Token: 0x04000119 RID: 281
		private readonly int int_0;
	}
}
