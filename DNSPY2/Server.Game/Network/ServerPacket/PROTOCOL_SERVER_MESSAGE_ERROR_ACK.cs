using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200010F RID: 271
	public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : GameServerPacket
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00004A29 File Offset: 0x00002C29
		public PROTOCOL_SERVER_MESSAGE_ERROR_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00004A38 File Offset: 0x00002C38
		public override void Write()
		{
			base.WriteH(3078);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001F9 RID: 505
		private readonly uint uint_0;
	}
}
