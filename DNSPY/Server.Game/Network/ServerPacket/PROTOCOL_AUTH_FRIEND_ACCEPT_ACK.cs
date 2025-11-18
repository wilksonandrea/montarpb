using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000030 RID: 48
	public class PROTOCOL_AUTH_FRIEND_ACCEPT_ACK : GameServerPacket
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00002C35 File Offset: 0x00000E35
		public PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002C44 File Offset: 0x00000E44
		public override void Write()
		{
			base.WriteH(1817);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400005C RID: 92
		private readonly uint uint_0;
	}
}
