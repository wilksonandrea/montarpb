using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000CD RID: 205
	public class PROTOCOL_CS_ROOM_INVITED_ACK : GameServerPacket
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00004195 File Offset: 0x00002395
		public PROTOCOL_CS_ROOM_INVITED_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000041A4 File Offset: 0x000023A4
		public override void Write()
		{
			base.WriteH(1902);
			base.WriteD(this.int_0);
		}

		// Token: 0x04000175 RID: 373
		private readonly int int_0;
	}
}
