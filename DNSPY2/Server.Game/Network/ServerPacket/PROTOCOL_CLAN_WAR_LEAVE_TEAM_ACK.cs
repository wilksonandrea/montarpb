using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000091 RID: 145
	public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK : GameServerPacket
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000385A File Offset: 0x00001A5A
		public PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003869 File Offset: 0x00001A69
		public override void Write()
		{
			base.WriteH(6923);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000117 RID: 279
		private readonly uint uint_0;
	}
}
