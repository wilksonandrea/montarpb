using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000FF RID: 255
	public class PROTOCOL_ROOM_LOADING_START_ACK : GameServerPacket
	{
		// Token: 0x06000270 RID: 624 RVA: 0x000048BC File Offset: 0x00002ABC
		public PROTOCOL_ROOM_LOADING_START_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000048CB File Offset: 0x00002ACB
		public override void Write()
		{
			base.WriteH(3658);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001DB RID: 475
		private readonly uint uint_0;
	}
}
