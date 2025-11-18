using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B0 RID: 176
	public class PROTOCOL_CS_DENIAL_REQUEST_ACK : GameServerPacket
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00003CCD File Offset: 0x00001ECD
		public PROTOCOL_CS_DENIAL_REQUEST_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00003CDC File Offset: 0x00001EDC
		public override void Write()
		{
			base.WriteH(826);
			base.WriteD(this.int_0);
		}

		// Token: 0x04000146 RID: 326
		private readonly int int_0;
	}
}
