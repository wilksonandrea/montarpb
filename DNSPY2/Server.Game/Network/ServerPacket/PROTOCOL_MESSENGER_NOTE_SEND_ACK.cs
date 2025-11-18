using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E6 RID: 230
	public class PROTOCOL_MESSENGER_NOTE_SEND_ACK : GameServerPacket
	{
		// Token: 0x06000234 RID: 564 RVA: 0x000044CF File Offset: 0x000026CF
		public PROTOCOL_MESSENGER_NOTE_SEND_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000044DE File Offset: 0x000026DE
		public override void Write()
		{
			base.WriteH(1922);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001AA RID: 426
		private readonly uint uint_0;
	}
}
