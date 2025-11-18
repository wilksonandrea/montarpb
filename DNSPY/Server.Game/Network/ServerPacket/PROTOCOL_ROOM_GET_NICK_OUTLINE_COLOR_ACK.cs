using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F2 RID: 242
	public class PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK : GameServerPacket
	{
		// Token: 0x0600024D RID: 589 RVA: 0x000046B2 File Offset: 0x000028B2
		public PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000046C8 File Offset: 0x000028C8
		public override void Write()
		{
			base.WriteH(3638);
			base.WriteD(this.int_0);
			base.WriteC((byte)this.int_1);
		}

		// Token: 0x040001BC RID: 444
		private readonly int int_0;

		// Token: 0x040001BD RID: 445
		private readonly int int_1;
	}
}
