using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly Account account_0;

		public PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(2367);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
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
	}
}