using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000054 RID: 84
	public class PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK : GameServerPacket
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000307F File Offset: 0x0000127F
		public PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000308E File Offset: 0x0000128E
		public override void Write()
		{
			base.WriteH(2427);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000AB RID: 171
		private readonly uint uint_0;
	}
}
