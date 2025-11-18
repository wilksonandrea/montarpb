using System;
using System.Collections.Generic;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000095 RID: 149
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK : GameServerPacket
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00003907 File Offset: 0x00001B07
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(List<MatchModel> list_1, int int_2)
		{
			this.int_0 = int_2;
			this.list_0 = list_1;
			this.int_1 = list_1.Count - 1;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		public override void Write()
		{
			base.WriteH(6917);
			base.WriteH((ushort)this.int_1);
			if (this.int_1 > 0)
			{
				base.WriteH(1);
				base.WriteH(0);
				base.WriteC((byte)this.int_1);
				foreach (MatchModel matchModel in this.list_0)
				{
					if (matchModel.MatchId != this.int_0)
					{
						base.WriteH((short)matchModel.MatchId);
						base.WriteH((short)matchModel.GetServerInfo());
						base.WriteH((short)matchModel.GetServerInfo());
						base.WriteC((byte)matchModel.State);
						base.WriteC((byte)matchModel.FriendId);
						base.WriteC((byte)matchModel.Training);
						base.WriteC((byte)matchModel.GetCountPlayers());
						base.WriteD(matchModel.Leader);
						base.WriteC(0);
						base.WriteD(matchModel.Clan.Id);
						base.WriteC((byte)matchModel.Clan.Rank);
						base.WriteD(matchModel.Clan.Logo);
						base.WriteS(matchModel.Clan.Name, 17);
						base.WriteT(matchModel.Clan.Points);
						base.WriteC((byte)matchModel.Clan.NameColor);
						Account leader = matchModel.GetLeader();
						if (leader != null)
						{
							base.WriteC((byte)leader.Rank);
							base.WriteS(leader.Nickname, 33);
							base.WriteQ(leader.PlayerId);
							base.WriteC((byte)matchModel.Slots[matchModel.Leader].State);
						}
						else
						{
							base.WriteB(new byte[43]);
						}
					}
				}
			}
		}

		// Token: 0x0400011E RID: 286
		private readonly List<MatchModel> list_0;

		// Token: 0x0400011F RID: 287
		private readonly int int_0;

		// Token: 0x04000120 RID: 288
		private readonly int int_1;
	}
}
