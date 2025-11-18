using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000027 RID: 39
	public class PROTOCOL_BASE_USER_TITLE_RELEASE_ACK : GameServerPacket
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00002A79 File Offset: 0x00000C79
		public PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(uint uint_1, int int_2, int int_3)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002A96 File Offset: 0x00000C96
		public override void Write()
		{
			base.WriteH(2381);
			base.WriteD(this.uint_0);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
		}

		// Token: 0x0400004D RID: 77
		private readonly uint uint_0;

		// Token: 0x0400004E RID: 78
		private readonly int int_0;

		// Token: 0x0400004F RID: 79
		private readonly int int_1;
	}
}
