using System;
using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200007C RID: 124
	public class PROTOCOL_BATTLE_START_COUNTDOWN_ACK : GameServerPacket
	{
		// Token: 0x0600014D RID: 333 RVA: 0x000035A7 File Offset: 0x000017A7
		public PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum countDownEnum_1)
		{
			this.countDownEnum_0 = countDownEnum_1;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000035B6 File Offset: 0x000017B6
		public override void Write()
		{
			base.WriteH(5126);
			base.WriteC((byte)this.countDownEnum_0);
		}

		// Token: 0x040000F5 RID: 245
		private readonly CountDownEnum countDownEnum_0;
	}
}
