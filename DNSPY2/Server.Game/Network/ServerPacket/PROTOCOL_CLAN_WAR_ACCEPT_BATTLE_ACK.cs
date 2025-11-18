using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000087 RID: 135
	public class PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK : GameServerPacket
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00003704 File Offset: 0x00001904
		public PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003713 File Offset: 0x00001913
		public override void Write()
		{
			base.WriteH(1559);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000108 RID: 264
		private readonly uint uint_0;
	}
}
