using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000097 RID: 151
	public class PROTOCOL_CLAN_WAR_RESULT_ACK : GameServerPacket
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CLAN_WAR_RESULT_ACK()
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Write()
		{
			base.WriteH(6966);
			base.WriteH(0);
		}
	}
}
