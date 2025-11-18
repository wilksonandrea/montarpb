using System;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200001A RID: 26
	public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK : GameServerPacket
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002895 File Offset: 0x00000A95
		public PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00009C6C File Offset: 0x00007E6C
		public override void Write()
		{
			base.WriteH(2367);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC((byte)this.account_0.Mission.ActualMission);
				base.WriteC((byte)this.account_0.Mission.Card1);
				base.WriteC((byte)this.account_0.Mission.Card2);
				base.WriteC((byte)this.account_0.Mission.Card3);
				base.WriteC((byte)this.account_0.Mission.Card4);
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission1, this.account_0.Mission.List1));
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission2, this.account_0.Mission.List2));
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission3, this.account_0.Mission.List3));
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission4, this.account_0.Mission.List4));
				base.WriteC((byte)this.account_0.Mission.Mission1);
				base.WriteC((byte)this.account_0.Mission.Mission2);
				base.WriteC((byte)this.account_0.Mission.Mission3);
				base.WriteC((byte)this.account_0.Mission.Mission4);
			}
		}

		// Token: 0x04000038 RID: 56
		private readonly uint uint_0;

		// Token: 0x04000039 RID: 57
		private readonly Account account_0;
	}
}
