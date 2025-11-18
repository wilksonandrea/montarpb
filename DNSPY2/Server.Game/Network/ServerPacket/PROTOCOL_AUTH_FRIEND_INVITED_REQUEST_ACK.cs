using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000036 RID: 54
	public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK : GameServerPacket
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00002D32 File Offset: 0x00000F32
		public PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002D41 File Offset: 0x00000F41
		public override void Write()
		{
			base.WriteH(1813);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x04000065 RID: 101
		private readonly int int_0;
	}
}
