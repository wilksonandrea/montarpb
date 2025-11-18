using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK : GameServerPacket
	{
		private readonly List<MatchModel> list_0;

		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(List<MatchModel> list_1, int int_2)
		{
			this.int_0 = int_2;
			this.list_0 = list_1;
			this.int_1 = list_1.Count - 1;
		}

		public override void Write()
		{
			base.WriteH(6917);
			base.WriteH((ushort)this.int_1);
			if (this.int_1 > 0)
			{
				base.WriteH(1);
				base.WriteH(0);
				base.WriteC((byte)this.int_1);
				foreach (MatchModel list0 in this.list_0)
				{
					if (list0.MatchId == this.int_0)
					{
						continue;
					}
					base.WriteH((short)list0.MatchId);
					base.WriteH((short)list0.GetServerInfo());
					base.WriteH((short)list0.GetServerInfo());
					base.WriteC((byte)list0.State);
					base.WriteC((byte)list0.FriendId);
					base.WriteC((byte)list0.Training);
					base.WriteC((byte)list0.GetCountPlayers());
					base.WriteD(list0.Leader);
					base.WriteC(0);
					base.WriteD(list0.Clan.Id);
					base.WriteC((byte)list0.Clan.Rank);
					base.WriteD(list0.Clan.Logo);
					base.WriteS(list0.Clan.Name, 17);
					base.WriteT(list0.Clan.Points);
					base.WriteC((byte)list0.Clan.NameColor);
					Account leader = list0.GetLeader();
					if (leader == null)
					{
						base.WriteB(new byte[43]);
					}
					else
					{
						base.WriteC((byte)leader.Rank);
						base.WriteS(leader.Nickname, 33);
						base.WriteQ(leader.PlayerId);
						base.WriteC((byte)list0.Slots[list0.Leader].State);
					}
				}
			}
		}
	}
}