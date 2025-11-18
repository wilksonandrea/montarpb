using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200007A RID: 122
	public class PROTOCOL_BATTLE_SENDPING_ACK : GameServerPacket
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000357F File Offset: 0x0000177F
		public PROTOCOL_BATTLE_SENDPING_ACK(byte[] byte_1)
		{
			this.byte_0 = byte_1;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000358E File Offset: 0x0000178E
		public override void Write()
		{
			base.WriteH(5147);
			base.WriteB(this.byte_0);
		}

		// Token: 0x040000F0 RID: 240
		private readonly byte[] byte_0;
	}
}
