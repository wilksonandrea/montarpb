using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly MatchModel matchModel_0;

		public PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(uint uint_1, MatchModel matchModel_1 = null)
		{
			this.uint_0 = uint_1;
			this.matchModel_0 = matchModel_1;
		}

		public override void Write()
		{
			base.WriteH(6919);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteH((short)this.matchModel_0.MatchId);
				base.WriteH((short)this.matchModel_0.GetServerInfo());
				base.WriteH((short)this.matchModel_0.GetServerInfo());
				base.WriteC((byte)this.matchModel_0.State);
				base.WriteC((byte)this.matchModel_0.FriendId);
				base.WriteC((byte)this.matchModel_0.Training);
				base.WriteC((byte)this.matchModel_0.GetCountPlayers());
				base.WriteD(this.matchModel_0.Leader);
				base.WriteC(0);
			}
		}
	}
}