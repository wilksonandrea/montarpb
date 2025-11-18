using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000031 RID: 49
	public class PROTOCOL_AUTH_FRIEND_DELETE_ACK : GameServerPacket
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00002C5D File Offset: 0x00000E5D
		public PROTOCOL_AUTH_FRIEND_DELETE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002C6C File Offset: 0x00000E6C
		public override void Write()
		{
			base.WriteH(1821);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400005D RID: 93
		private readonly uint uint_0;
	}
}
