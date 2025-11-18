using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000102 RID: 258
	public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000490C File Offset: 0x00002B0C
		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000490C File Offset: 0x00002B0C
		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(int int_0)
		{
			this.uint_0 = (uint)int_0;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000491B File Offset: 0x00002B1B
		public override void Write()
		{
			base.WriteH(3614);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001DD RID: 477
		private readonly uint uint_0;
	}
}
