using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E4 RID: 228
	public class PROTOCOL_MESSENGER_NOTE_DELETE_ACK : GameServerPacket
	{
		// Token: 0x0600022F RID: 559 RVA: 0x000044AA File Offset: 0x000026AA
		public PROTOCOL_MESSENGER_NOTE_DELETE_ACK(uint uint_1, List<object> list_1)
		{
			this.uint_0 = uint_1;
			this.list_0 = list_1;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00011B30 File Offset: 0x0000FD30
		public override void Write()
		{
			base.WriteH(1929);
			base.WriteD(this.uint_0);
			base.WriteC((byte)this.list_0.Count);
			foreach (object obj in this.list_0)
			{
				long num = (long)obj;
				base.WriteD((uint)num);
			}
		}

		// Token: 0x040001A7 RID: 423
		private readonly uint uint_0;

		// Token: 0x040001A8 RID: 424
		private readonly List<object> list_0;
	}
}
