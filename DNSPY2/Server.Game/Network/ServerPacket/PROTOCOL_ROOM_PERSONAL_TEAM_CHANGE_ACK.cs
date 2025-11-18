using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000100 RID: 256
	public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK()
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000139EC File Offset: 0x00011BEC
		public override void Write()
		{
			base.WriteH(3610);
			base.WriteC(2);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(2);
			base.WriteC(0);
			base.WriteC(8);
		}
	}
}
