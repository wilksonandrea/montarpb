using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000EB RID: 235
	public class PROTOCOL_ROOM_CHANGE_SLOT_ACK : GameServerPacket
	{
		// Token: 0x0600023F RID: 575 RVA: 0x000045A2 File Offset: 0x000027A2
		public PROTOCOL_ROOM_CHANGE_SLOT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000045B1 File Offset: 0x000027B1
		public override void Write()
		{
			base.WriteH(3605);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001B0 RID: 432
		private readonly uint uint_0;
	}
}
