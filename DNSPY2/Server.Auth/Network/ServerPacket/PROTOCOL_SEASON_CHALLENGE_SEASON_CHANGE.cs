using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000014 RID: 20
	public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : AuthServerPacket
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE()
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002600 File Offset: 0x00000800
		public override void Write()
		{
			base.WriteH(8451);
			base.WriteH(0);
			base.WriteC(1);
		}
	}
}
