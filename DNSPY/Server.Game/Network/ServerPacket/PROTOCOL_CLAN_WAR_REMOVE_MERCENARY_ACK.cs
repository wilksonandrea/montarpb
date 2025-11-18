using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200002C RID: 44
	public class PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK : GameServerPacket
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK()
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002B82 File Offset: 0x00000D82
		public override void Write()
		{
			base.WriteH(6941);
			base.WriteD(0);
		}
	}
}
