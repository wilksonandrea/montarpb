using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000CE RID: 206
	public class PROTOCOL_CS_ROOM_INVITED_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001FE RID: 510 RVA: 0x000041BD File Offset: 0x000023BD
		public PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(long long_1)
		{
			this.long_0 = long_1;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000041CC File Offset: 0x000023CC
		public override void Write()
		{
			base.WriteH(1903);
			base.WriteQ(this.long_0);
		}

		// Token: 0x04000176 RID: 374
		private readonly long long_0;
	}
}
