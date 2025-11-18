using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200009E RID: 158
	public class PROTOCOL_CS_CHECK_DUPLICATE_ACK : GameServerPacket
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00003A65 File Offset: 0x00001C65
		public PROTOCOL_CS_CHECK_DUPLICATE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003A74 File Offset: 0x00001C74
		public override void Write()
		{
			base.WriteH(917);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000130 RID: 304
		private readonly uint uint_0;
	}
}
