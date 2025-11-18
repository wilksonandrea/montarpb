using System;
using System.Collections.Generic;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A3 RID: 163
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK : GameServerPacket
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00003B68 File Offset: 0x00001D68
		public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK(int int_1, List<MatchModel> list_1)
		{
			this.int_0 = int_1;
			this.list_0 = list_1;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public override void Write()
		{
			base.WriteH(1957);
			base.WriteC((byte)((this.int_0 == 0) ? this.list_0.Count : this.int_0));
			if (this.int_0 <= 0 && this.list_0.Count != 0)
			{
				base.WriteC(1);
				base.WriteC(0);
				base.WriteC((byte)this.list_0.Count);
				for (int i = 0; i < this.list_0.Count; i++)
				{
					MatchModel matchModel = this.list_0[i];
					base.WriteH((short)matchModel.MatchId);
					base.WriteH((ushort)matchModel.GetServerInfo());
					base.WriteH((ushort)matchModel.GetServerInfo());
					base.WriteC((byte)matchModel.State);
					base.WriteC((byte)matchModel.FriendId);
					base.WriteC((byte)matchModel.Training);
					base.WriteC((byte)matchModel.GetCountPlayers());
					base.WriteC(0);
					base.WriteD(matchModel.Leader);
					Account leader = matchModel.GetLeader();
					if (leader != null)
					{
						base.WriteC((byte)leader.Rank);
						base.WriteU(leader.Nickname, 66);
						base.WriteQ(leader.PlayerId);
						base.WriteC((byte)matchModel.Slots[matchModel.Leader].State);
					}
					else
					{
						base.WriteB(new byte[76]);
					}
				}
				return;
			}
		}

		// Token: 0x04000138 RID: 312
		private readonly List<MatchModel> list_0;

		// Token: 0x04000139 RID: 313
		private readonly int int_0;
	}
}
