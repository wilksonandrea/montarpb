using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000084 RID: 132
	public class PROTOCOL_CHAR_CHANGE_EQUIP_ACK : GameServerPacket
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00003691 File Offset: 0x00001891
		public PROTOCOL_CHAR_CHANGE_EQUIP_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000036A0 File Offset: 0x000018A0
		public override void Write()
		{
			base.WriteH(6150);
			base.WriteD(this.uint_0);
			base.WriteH(0);
		}

		// Token: 0x04000100 RID: 256
		private readonly uint uint_0;
	}
}
