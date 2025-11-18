using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F4 RID: 244
	public class PROTOCOL_ROOM_GET_NICKNAME_ACK : GameServerPacket
	{
		// Token: 0x06000252 RID: 594 RVA: 0x000046EE File Offset: 0x000028EE
		public PROTOCOL_ROOM_GET_NICKNAME_ACK(int int_1, string string_1)
		{
			this.int_0 = int_1;
			this.string_0 = string_1;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00004704 File Offset: 0x00002904
		public override void Write()
		{
			base.WriteH(3599);
			base.WriteD(this.int_0);
			base.WriteU(this.string_0, 66);
		}

		// Token: 0x040001C0 RID: 448
		private readonly int int_0;

		// Token: 0x040001C1 RID: 449
		private readonly string string_0;
	}
}
