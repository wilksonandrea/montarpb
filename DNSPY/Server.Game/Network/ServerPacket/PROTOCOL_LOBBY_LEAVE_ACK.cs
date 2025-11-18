using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000DD RID: 221
	public class PROTOCOL_LOBBY_LEAVE_ACK : GameServerPacket
	{
		// Token: 0x0600021F RID: 543 RVA: 0x000043E8 File Offset: 0x000025E8
		public PROTOCOL_LOBBY_LEAVE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000043F7 File Offset: 0x000025F7
		public override void Write()
		{
			base.WriteH(2562);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000193 RID: 403
		private readonly uint uint_0;
	}
}
