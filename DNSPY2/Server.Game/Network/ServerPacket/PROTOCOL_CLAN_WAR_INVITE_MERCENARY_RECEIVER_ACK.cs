using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200008E RID: 142
	public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK : GameServerPacket
	{
		// Token: 0x06000174 RID: 372 RVA: 0x000037AB File Offset: 0x000019AB
		public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(uint uint_1, int int_1 = 0)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000037C1 File Offset: 0x000019C1
		public override void Write()
		{
			base.WriteH(1572);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC((byte)this.int_0);
			}
		}

		// Token: 0x04000110 RID: 272
		private readonly int int_0;

		// Token: 0x04000111 RID: 273
		private readonly uint uint_0;
	}
}
