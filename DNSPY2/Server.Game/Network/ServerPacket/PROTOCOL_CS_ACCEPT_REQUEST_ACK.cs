using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000099 RID: 153
	public class PROTOCOL_CS_ACCEPT_REQUEST_ACK : GameServerPacket
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000397A File Offset: 0x00001B7A
		public PROTOCOL_CS_ACCEPT_REQUEST_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003989 File Offset: 0x00001B89
		public override void Write()
		{
			base.WriteH(823);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000126 RID: 294
		private readonly uint uint_0;
	}
}
