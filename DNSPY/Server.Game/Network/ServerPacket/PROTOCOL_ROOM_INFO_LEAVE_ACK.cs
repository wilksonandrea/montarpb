using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000FC RID: 252
	public class PROTOCOL_ROOM_INFO_LEAVE_ACK : GameServerPacket
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_ROOM_INFO_LEAVE_ACK()
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000484E File Offset: 0x00002A4E
		public override void Write()
		{
			base.WriteH(3674);
			base.WriteD(0);
			base.WriteC(68);
		}
	}
}
