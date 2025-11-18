using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000FD RID: 253
	public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK : GameServerPacket
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000486A File Offset: 0x00002A6A
		public PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00004879 File Offset: 0x00002A79
		public override void Write()
		{
			base.WriteH(3630);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001D7 RID: 471
		private readonly uint uint_0;
	}
}
