using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000079 RID: 121
	public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK : GameServerPacket
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00003557 File Offset: 0x00001757
		public PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003566 File Offset: 0x00001766
		public override void Write()
		{
			base.WriteH(5175);
			base.WriteD(this.int_0);
		}

		// Token: 0x040000EF RID: 239
		private readonly int int_0;
	}
}
