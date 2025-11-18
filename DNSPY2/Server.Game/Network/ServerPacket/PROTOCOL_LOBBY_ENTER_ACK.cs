using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000DA RID: 218
	public class PROTOCOL_LOBBY_ENTER_ACK : GameServerPacket
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00004394 File Offset: 0x00002594
		public PROTOCOL_LOBBY_ENTER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000043A3 File Offset: 0x000025A3
		public override void Write()
		{
			base.WriteH(2584);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			base.WriteC(0);
			base.WriteQ(0L);
		}

		// Token: 0x04000189 RID: 393
		private readonly uint uint_0;
	}
}
