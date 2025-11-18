using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000118 RID: 280
	public class PROTOCOL_SHOP_FLASH_SALE_LIST_ACK : GameServerPacket
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_FLASH_SALE_LIST_ACK()
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00004B64 File Offset: 0x00002D64
		public override void Write()
		{
			base.WriteH(1111);
			base.WriteC(1);
			base.WriteD(1);
			base.WriteC(1);
		}
	}
}
