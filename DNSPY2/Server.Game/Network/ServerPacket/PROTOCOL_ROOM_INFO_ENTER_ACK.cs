using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000FB RID: 251
	public class PROTOCOL_ROOM_INFO_ENTER_ACK : GameServerPacket
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_ROOM_INFO_ENTER_ACK()
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00004832 File Offset: 0x00002A32
		public override void Write()
		{
			base.WriteH(3672);
			base.WriteD(0);
			base.WriteC(68);
		}
	}
}
