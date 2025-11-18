using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200010A RID: 266
	public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : GameServerPacket
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE()
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000049D3 File Offset: 0x00002BD3
		public override void Write()
		{
			base.WriteH(8451);
			base.WriteH(0);
			base.WriteC(1);
		}
	}
}
