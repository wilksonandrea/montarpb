using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200011B RID: 283
	public class PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK : GameServerPacket
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK()
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0001459C File Offset: 0x0001279C
		public override void Write()
		{
			base.WriteH(1096);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteC(1);
			base.WriteD(63266001);
			base.WriteC(1);
			base.WriteD(1512052359);
			base.WriteC(1);
		}
	}
}
