using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000101 RID: 257
	public class PROTOCOL_ROOM_REQUEST_MAIN_ACK : GameServerPacket
	{
		// Token: 0x06000274 RID: 628 RVA: 0x000048E4 File Offset: 0x00002AE4
		public PROTOCOL_ROOM_REQUEST_MAIN_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000048E4 File Offset: 0x00002AE4
		public PROTOCOL_ROOM_REQUEST_MAIN_ACK(int int_0)
		{
			this.uint_0 = (uint)int_0;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000048F3 File Offset: 0x00002AF3
		public override void Write()
		{
			base.WriteH(3612);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001DC RID: 476
		private readonly uint uint_0;
	}
}
