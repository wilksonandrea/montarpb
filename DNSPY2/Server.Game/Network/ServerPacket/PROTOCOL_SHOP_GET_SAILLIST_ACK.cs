using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000116 RID: 278
	public class PROTOCOL_SHOP_GET_SAILLIST_ACK : GameServerPacket
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00004B08 File Offset: 0x00002D08
		public PROTOCOL_SHOP_GET_SAILLIST_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00004B17 File Offset: 0x00002D17
		public override void Write()
		{
			base.WriteH(1030);
			base.WriteC((this.bool_0 > false) ? 1 : 0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}

		// Token: 0x040001FE RID: 510
		private readonly bool bool_0;
	}
}
