using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000EF RID: 239
	public class PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK : GameServerPacket
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000462D File Offset: 0x0000282D
		public PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00004643 File Offset: 0x00002843
		public override void Write()
		{
			base.WriteH(3670);
			base.WriteD(this.int_0);
			base.WriteC((byte)this.int_1);
		}

		// Token: 0x040001B8 RID: 440
		private readonly int int_0;

		// Token: 0x040001B9 RID: 441
		private readonly int int_1;
	}
}
