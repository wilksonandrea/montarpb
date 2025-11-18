using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000043 RID: 67
	public class PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK : GameServerPacket
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00002E9B File Offset: 0x0000109B
		public PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002EAA File Offset: 0x000010AA
		public override void Write()
		{
			base.WriteH(1088);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000087 RID: 135
		private readonly uint uint_0;
	}
}
