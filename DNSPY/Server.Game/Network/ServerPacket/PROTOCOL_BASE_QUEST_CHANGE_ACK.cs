using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000019 RID: 25
	public class PROTOCOL_BASE_QUEST_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002838 File Offset: 0x00000A38
		public PROTOCOL_BASE_QUEST_CHANGE_ACK(int int_2, MissionCardModel missionCardModel_0)
		{
			this.int_0 = missionCardModel_0.MissionBasicId;
			if (missionCardModel_0.MissionLimit == int_2)
			{
				this.int_0 += 240;
			}
			this.int_1 = int_2;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000286E File Offset: 0x00000A6E
		public override void Write()
		{
			base.WriteH(2359);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
		}

		// Token: 0x04000036 RID: 54
		private readonly int int_0;

		// Token: 0x04000037 RID: 55
		private readonly int int_1;
	}
}
