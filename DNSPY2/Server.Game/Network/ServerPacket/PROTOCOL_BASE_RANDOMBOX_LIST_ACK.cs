using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200001D RID: 29
	public class PROTOCOL_BASE_RANDOMBOX_LIST_ACK : GameServerPacket
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000291A File Offset: 0x00000B1A
		public PROTOCOL_BASE_RANDOMBOX_LIST_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002929 File Offset: 0x00000B29
		public override void Write()
		{
			base.WriteH(2499);
			base.WriteC((this.bool_0 > false) ? 1 : 0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}

		// Token: 0x0400003D RID: 61
		private readonly bool bool_0;
	}
}
