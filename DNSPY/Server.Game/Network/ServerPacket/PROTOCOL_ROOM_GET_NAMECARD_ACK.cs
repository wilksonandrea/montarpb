using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F1 RID: 241
	public class PROTOCOL_ROOM_GET_NAMECARD_ACK : GameServerPacket
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_ROOM_GET_NAMECARD_ACK()
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000046A5 File Offset: 0x000028A5
		public override void Write()
		{
			base.WriteH(3637);
		}
	}
}
