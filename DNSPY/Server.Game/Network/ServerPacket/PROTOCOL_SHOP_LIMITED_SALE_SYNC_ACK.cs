using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200011C RID: 284
	public class PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK : GameServerPacket
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK()
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00004C05 File Offset: 0x00002E05
		public override void Write()
		{
			base.WriteH(1098);
			base.WriteC(1);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}
