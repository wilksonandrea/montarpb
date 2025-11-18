using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C4 RID: 196
	public class PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00004015 File Offset: 0x00002215
		public PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00004024 File Offset: 0x00002224
		public override void Write()
		{
			base.WriteH(980);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x04000165 RID: 357
		private readonly int int_0;
	}
}
