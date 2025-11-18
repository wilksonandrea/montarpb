using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000090 RID: 144
	public class PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK : GameServerPacket
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00003844 File Offset: 0x00001A44
		public PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(uint uint_1, MatchModel matchModel_1 = null)
		{
			this.uint_0 = uint_1;
			this.matchModel_0 = matchModel_1;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		public override void Write()
		{
			base.WriteH(6921);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteH((short)this.matchModel_0.MatchId);
				base.WriteH((ushort)this.matchModel_0.GetServerInfo());
				base.WriteH((ushort)this.matchModel_0.GetServerInfo());
				base.WriteC((byte)this.matchModel_0.State);
				base.WriteC((byte)this.matchModel_0.FriendId);
				base.WriteC((byte)this.matchModel_0.Training);
				base.WriteC((byte)this.matchModel_0.GetCountPlayers());
				base.WriteD(this.matchModel_0.Leader);
				base.WriteC(0);
				base.WriteD(this.matchModel_0.Clan.Id);
				base.WriteC((byte)this.matchModel_0.Clan.Rank);
				base.WriteD(this.matchModel_0.Clan.Logo);
				base.WriteS(this.matchModel_0.Clan.Name, 17);
				base.WriteT(this.matchModel_0.Clan.Points);
				base.WriteC((byte)this.matchModel_0.Clan.NameColor);
				for (int i = 0; i < this.matchModel_0.Training; i++)
				{
					SlotMatch slotMatch = this.matchModel_0.Slots[i];
					Account playerBySlot = this.matchModel_0.GetPlayerBySlot(slotMatch);
					if (playerBySlot != null)
					{
						base.WriteC((byte)playerBySlot.Rank);
						base.WriteS(playerBySlot.Nickname, 33);
						base.WriteQ(playerBySlot.PlayerId);
						base.WriteC((byte)slotMatch.State);
					}
					else
					{
						base.WriteB(new byte[43]);
					}
				}
			}
		}

		// Token: 0x04000115 RID: 277
		private readonly MatchModel matchModel_0;

		// Token: 0x04000116 RID: 278
		private readonly uint uint_0;
	}
}
