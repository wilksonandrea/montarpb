using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000072 RID: 114
	public class PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK : GameServerPacket
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000034B8 File Offset: 0x000016B8
		public PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000034C7 File Offset: 0x000016C7
		public override void Write()
		{
			base.WriteH(3413);
			base.WriteC((byte)this.int_0);
			base.WriteC(1);
			base.WriteD(1);
		}

		// Token: 0x040000DF RID: 223
		private readonly int int_0;
	}
}
