using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly StatisticAcemode statisticAcemode_0;

	private readonly ClanModel clanModel_0;

	public PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(Account account_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			statisticAcemode_0 = account_1.Statistic.Acemode;
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
		}
	}

	public override void Write()
	{
		WriteH(3681);
		WriteH(0);
		WriteD((uint)account_0.PlayerId);
		WriteD(statisticAcemode_0.Matches);
		WriteD(statisticAcemode_0.MatchWins);
		WriteD(statisticAcemode_0.MatchLoses);
		WriteD(statisticAcemode_0.Kills);
		WriteD(statisticAcemode_0.Deaths);
		WriteD(statisticAcemode_0.Headshots);
		WriteD(statisticAcemode_0.Assists);
		WriteD(statisticAcemode_0.Escapes);
		WriteD(statisticAcemode_0.Winstreaks);
		WriteB(new byte[122]);
		WriteU(account_0.Nickname, 66);
		WriteD(account_0.Rank);
		WriteD(account_0.GetRank());
		WriteD(0);
		WriteD(0);
		WriteD(account_0.Gold);
		WriteD(account_0.Exp);
		WriteD(account_0.Tags);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteD(account_0.Cash);
		WriteD(account_0.ClanId);
		WriteD(account_0.ClanAccess);
		WriteQ(account_0.StatusId());
		WriteC((byte)account_0.CafePC);
		WriteC((byte)account_0.Country);
		WriteU(clanModel_0.Name, 34);
		WriteC((byte)clanModel_0.Rank);
		WriteC((byte)clanModel_0.GetClanUnit());
		WriteD(clanModel_0.Logo);
		WriteC((byte)clanModel_0.NameColor);
		WriteC((byte)clanModel_0.Effect);
		WriteC((byte)(GameXender.Client.Config.EnableBlood ? ((uint)account_0.Age) : 24u));
	}
}
