using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200011A RID: 282
	public class PROTOCOL_SHOP_ACCOUNT_LIMITED_SALE_ACK : GameServerPacket
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_ACCOUNT_LIMITED_SALE_ACK()
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00004BDC File Offset: 0x00002DDC
		public override void Write()
		{
			base.WriteH(1101);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
		}
	}
}
