using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000111 RID: 273
	public class PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK : GameServerPacket
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00004A67 File Offset: 0x00002C67
		public PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00004A76 File Offset: 0x00002C76
		public override void Write()
		{
			base.WriteH(3084);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001FC RID: 508
		private readonly uint uint_0;
	}
}
