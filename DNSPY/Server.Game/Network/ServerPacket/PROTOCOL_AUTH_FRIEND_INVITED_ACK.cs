using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000035 RID: 53
	public class PROTOCOL_AUTH_FRIEND_INVITED_ACK : GameServerPacket
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00002D0A File Offset: 0x00000F0A
		public PROTOCOL_AUTH_FRIEND_INVITED_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002D19 File Offset: 0x00000F19
		public override void Write()
		{
			base.WriteH(1812);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000064 RID: 100
		private readonly uint uint_0;
	}
}
