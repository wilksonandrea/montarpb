using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000AC RID: 172
	public class PROTOCOL_CS_COMMISSION_STAFF_ACK : GameServerPacket
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00003C57 File Offset: 0x00001E57
		public PROTOCOL_CS_COMMISSION_STAFF_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00003C66 File Offset: 0x00001E66
		public override void Write()
		{
			base.WriteH(837);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000142 RID: 322
		private readonly uint uint_0;
	}
}
