using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200011F RID: 287
	public class PROTOCOL_SHOP_TAG_INFO_ACK : GameServerPacket
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_TAG_INFO_ACK()
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000146C0 File Offset: 0x000128C0
		public override void Write()
		{
			base.WriteH(1099);
			base.WriteH(0);
			base.WriteC(7);
			base.WriteC(5);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteD(0);
			base.WriteH(0);
			base.WriteC(3);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(4);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(2);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(6);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(1);
			base.WriteQ(0L);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteC(byte.MaxValue);
			base.WriteC(byte.MaxValue);
			base.WriteC(byte.MaxValue);
			base.WriteC(0);
			base.WriteC(byte.MaxValue);
			base.WriteC(1);
			base.WriteC(7);
			base.WriteC(2);
		}
	}
}
