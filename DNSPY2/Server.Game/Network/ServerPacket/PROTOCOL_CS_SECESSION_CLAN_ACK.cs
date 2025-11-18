using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000CF RID: 207
	public class PROTOCOL_CS_SECESSION_CLAN_ACK : GameServerPacket
	{
		// Token: 0x06000200 RID: 512 RVA: 0x000041E5 File Offset: 0x000023E5
		public PROTOCOL_CS_SECESSION_CLAN_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000041F4 File Offset: 0x000023F4
		public override void Write()
		{
			base.WriteH(829);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000177 RID: 375
		private readonly uint uint_0;
	}
}
