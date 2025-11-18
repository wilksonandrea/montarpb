using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200002F RID: 47
	public class PROTOCOL_AUTH_FIND_USER_ACK : GameServerPacket
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00002C06 File Offset: 0x00000E06
		public PROTOCOL_AUTH_FIND_USER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002C15 File Offset: 0x00000E15
		public override void Write()
		{
			base.WriteH(1834);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400005B RID: 91
		private readonly uint uint_0;
	}
}
