using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000115 RID: 277
	public class PROTOCOL_SHOP_ENTER_ACK : GameServerPacket
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_ENTER_ACK()
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public override void Write()
		{
			base.WriteH(1026);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}
