using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000066 RID: 102
	public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK : GameServerPacket
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000333A File Offset: 0x0000153A
		public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003349 File Offset: 0x00001549
		public override void Write()
		{
			base.WriteH(5159);
			base.WriteD(this.int_0);
		}

		// Token: 0x040000D0 RID: 208
		private readonly int int_0;
	}
}
