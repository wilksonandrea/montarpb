using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000060 RID: 96
	public class PROTOCOL_BATTLE_COUNT_DOWN_ACK : GameServerPacket
	{
		// Token: 0x06000101 RID: 257 RVA: 0x0000327C File Offset: 0x0000147C
		public PROTOCOL_BATTLE_COUNT_DOWN_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000328B File Offset: 0x0000148B
		public override void Write()
		{
			base.WriteH(5275);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x040000BB RID: 187
		private readonly int int_0;
	}
}
