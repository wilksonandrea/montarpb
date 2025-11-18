using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000014 RID: 20
	public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK : GameServerPacket
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000279A File Offset: 0x0000099A
		public PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000027A9 File Offset: 0x000009A9
		public override void Write()
		{
			base.WriteH(3650);
			base.WriteD(this.int_0);
		}

		// Token: 0x0400002C RID: 44
		private readonly int int_0;
	}
}
