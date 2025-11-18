using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200010E RID: 270
	public class PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK : GameServerPacket
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00004A13 File Offset: 0x00002C13
		public PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(uint uint_1, bool bool_1)
		{
			this.uint_0 = uint_1;
			this.bool_0 = bool_1;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000144E4 File Offset: 0x000126E4
		public override void Write()
		{
			base.WriteH(3074);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
			base.WriteD(this.uint_0);
			base.WriteD((int)((this.bool_0 > false) ? 1 : 0));
			if (this.bool_0)
			{
				base.WriteD(0);
			}
		}

		// Token: 0x040001F7 RID: 503
		private readonly uint uint_0;

		// Token: 0x040001F8 RID: 504
		private readonly bool bool_0;
	}
}
