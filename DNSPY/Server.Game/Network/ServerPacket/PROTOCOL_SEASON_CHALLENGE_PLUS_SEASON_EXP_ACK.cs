using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000109 RID: 265
	public class PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK : GameServerPacket
	{
		// Token: 0x06000287 RID: 647 RVA: 0x000049C4 File Offset: 0x00002BC4
		public PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00013ED0 File Offset: 0x000120D0
		public override void Write()
		{
			base.WriteH(8456);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			base.WriteC(1);
			base.WriteC(6);
			base.WriteD(2580);
			base.WriteC(5);
			base.WriteC(5);
			base.WriteC(1);
		}

		// Token: 0x040001EB RID: 491
		private readonly uint uint_0;
	}
}
