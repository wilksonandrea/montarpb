using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000024 RID: 36
	public class PROTOCOL_BASE_USER_TITLE_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000029DF File Offset: 0x00000BDF
		public PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(uint uint_1, int int_1)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000029F5 File Offset: 0x00000BF5
		public override void Write()
		{
			base.WriteH(2377);
			base.WriteD(this.uint_0);
			base.WriteD(this.int_0);
		}

		// Token: 0x04000047 RID: 71
		private readonly int int_0;

		// Token: 0x04000048 RID: 72
		private readonly uint uint_0;
	}
}
