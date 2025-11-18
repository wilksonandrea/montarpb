using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200009B RID: 155
	public class PROTOCOL_CS_CANCEL_REQUEST_ACK : GameServerPacket
	{
		// Token: 0x06000191 RID: 401 RVA: 0x000039E8 File Offset: 0x00001BE8
		public PROTOCOL_CS_CANCEL_REQUEST_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000039F7 File Offset: 0x00001BF7
		public override void Write()
		{
			base.WriteH(815);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400012A RID: 298
		private readonly uint uint_0;
	}
}
