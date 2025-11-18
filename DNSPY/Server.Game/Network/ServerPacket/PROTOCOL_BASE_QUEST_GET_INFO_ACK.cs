using System;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200001B RID: 27
	public class PROTOCOL_BASE_QUEST_GET_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000028AB File Offset: 0x00000AAB
		public PROTOCOL_BASE_QUEST_GET_INFO_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00009E18 File Offset: 0x00008018
		public override void Write()
		{
			base.WriteH(2355);
			base.WriteC((byte)this.account_0.Mission.ActualMission);
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

		// Token: 0x0400003A RID: 58
		private readonly Account account_0;
	}
}
