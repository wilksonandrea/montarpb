using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000114 RID: 276
	public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : GameServerPacket
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK()
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00004ACB File Offset: 0x00002CCB
		public override void Write()
		{
			base.WriteH(3086);
		}
	}
}
