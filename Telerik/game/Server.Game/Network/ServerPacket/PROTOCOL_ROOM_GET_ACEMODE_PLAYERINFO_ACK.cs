using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly StatisticAcemode statisticAcemode_0;

		private readonly ClanModel clanModel_0;

		public PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.statisticAcemode_0 = account_1.Statistic.Acemode;
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			}
		}

		public override void Write()
		{
			base.WriteH(3681);
			base.WriteH(0);
			base.WriteD((uint)this.account_0.PlayerId);
			base.WriteD(this.statisticAcemode_0.Matches);
			base.WriteD(this.statisticAcemode_0.MatchWins);
			base.WriteD(this.statisticAcemode_0.MatchLoses);
			base.WriteD(this.statisticAcemode_0.Kills);
			base.WriteD(this.statisticAcemode_0.Deaths);
			base.WriteD(this.statisticAcemode_0.Headshots);
			base.WriteD(this.statisticAcemode_0.Assists);
			base.WriteD(this.statisticAcemode_0.Escapes);
			base.WriteD(this.statisticAcemode_0.Winstreaks);
			base.WriteB(new byte[122]);
			base.WriteU(this.account_0.Nickname, 66);
			base.WriteD(this.account_0.Rank);
			base.WriteD(this.account_0.GetRank());
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(this.account_0.Gold);
			base.WriteD(this.account_0.Exp);
			base.WriteD(this.account_0.Tags);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteD(this.account_0.Cash);
			base.WriteD(this.account_0.ClanId);
			base.WriteD(this.account_0.ClanAccess);
			base.WriteQ(this.account_0.StatusId());
			base.WriteC((byte)this.account_0.CafePC);
			base.WriteC((byte)this.account_0.Country);
			base.WriteU(this.clanModel_0.Name, 34);
			base.WriteC((byte)this.clanModel_0.Rank);
			base.WriteC((byte)this.clanModel_0.GetClanUnit());
			base.WriteD(this.clanModel_0.Logo);
			base.WriteC((byte)this.clanModel_0.NameColor);
			base.WriteC((byte)this.clanModel_0.Effect);
			base.WriteC((byte)((GameXender.Client.Config.EnableBlood ? this.account_0.Age : 24)));
		}
	}
}