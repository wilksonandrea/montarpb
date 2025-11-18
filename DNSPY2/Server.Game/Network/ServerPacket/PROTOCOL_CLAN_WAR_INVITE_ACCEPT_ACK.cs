using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200008D RID: 141
	public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK : GameServerPacket
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00003783 File Offset: 0x00001983
		public PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003792 File Offset: 0x00001992
		public override void Write()
		{
			base.WriteH(1560);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400010F RID: 271
		private readonly uint uint_0;
	}
}
