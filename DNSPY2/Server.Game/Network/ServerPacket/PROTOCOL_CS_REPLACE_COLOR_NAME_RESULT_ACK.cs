using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C1 RID: 193
	public class PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00003F9C File Offset: 0x0000219C
		public PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00003FAB File Offset: 0x000021AB
		public override void Write()
		{
			base.WriteH(902);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x04000162 RID: 354
		private readonly int int_0;
	}
}
