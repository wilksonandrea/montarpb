using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B1 RID: 177
	public class PROTOCOL_CS_DEPORTATION_ACK : GameServerPacket
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public PROTOCOL_CS_DEPORTATION_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00003D04 File Offset: 0x00001F04
		public override void Write()
		{
			base.WriteH(831);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000147 RID: 327
		private readonly uint uint_0;
	}
}
