using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK : GameServerPacket
	{
		private readonly List<MatchModel> list_0;

		private readonly int int_0;

		public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK(int int_1, List<MatchModel> list_1)
		{
			this.int_0 = int_1;
			this.list_0 = list_1;
		}

		public override void Write()
		{
			base.WriteH(1957);
			base.WriteC((byte)((this.int_0 == 0 ? this.list_0.Count : this.int_0)));
			if (this.int_0 > 0 || this.list_0.Count == 0)
			{
				return;
			}
			base.WriteC(1);
			base.WriteC(0);
			base.WriteC((byte)this.list_0.Count);
			for (int i = 0; i < this.list_0.Count; i++)
			{
				MatchModel ıtem = this.list_0[i];
				base.WriteH((short)ıtem.MatchId);
				base.WriteH((ushort)ıtem.GetServerInfo());
				base.WriteH((ushort)ıtem.GetServerInfo());
				base.WriteC((byte)ıtem.State);
				base.WriteC((byte)ıtem.FriendId);
				base.WriteC((byte)ıtem.Training);
				base.WriteC((byte)ıtem.GetCountPlayers());
				base.WriteC(0);
				base.WriteD(ıtem.Leader);
				Account leader = ıtem.GetLeader();
				if (leader == null)
				{
					base.WriteB(new byte[76]);
				}
				else
				{
					base.WriteC((byte)leader.Rank);
					base.WriteU(leader.Nickname, 66);
					base.WriteQ(leader.PlayerId);
					base.WriteC((byte)ıtem.Slots[ıtem.Leader].State);
				}
			}
		}
	}
}