using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200009F RID: 159
	public class PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK : GameServerPacket
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00003A8D File Offset: 0x00001C8D
		public PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003A9C File Offset: 0x00001C9C
		public override void Write()
		{
			base.WriteH(811);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000131 RID: 305
		private readonly uint uint_0;
	}
}
