using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E3 RID: 227
	public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK : GameServerPacket
	{
		// Token: 0x0600022D RID: 557 RVA: 0x0000449B File Offset: 0x0000269B
		public PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(List<int> list_1)
		{
			this.list_0 = list_1;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00011ADC File Offset: 0x0000FCDC
		public override void Write()
		{
			base.WriteH(1927);
			base.WriteC((byte)this.list_0.Count);
			for (int i = 0; i < this.list_0.Count; i++)
			{
				base.WriteD(this.list_0[i]);
			}
		}

		// Token: 0x040001A6 RID: 422
		private readonly List<int> list_0;
	}
}
