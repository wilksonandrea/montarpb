using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000117 RID: 279
	public class PROTOCOL_SHOP_LEAVE_ACK : GameServerPacket
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SHOP_LEAVE_ACK()
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00004B49 File Offset: 0x00002D49
		public override void Write()
		{
			base.WriteH(1028);
			base.WriteH(0);
			base.WriteD(0);
		}
	}
}
