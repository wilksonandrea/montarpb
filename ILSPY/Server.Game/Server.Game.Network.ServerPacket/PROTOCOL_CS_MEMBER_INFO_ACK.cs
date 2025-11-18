using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_ACK : GameServerPacket
{
	private readonly List<Account> list_0;

	public PROTOCOL_CS_MEMBER_INFO_ACK(List<Account> list_1)
	{
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(845);
		WriteC((byte)list_0.Count);
		foreach (Account item in list_0)
		{
			WriteC((byte)(item.Nickname.Length + 1));
			WriteN(item.Nickname, item.Nickname.Length + 2, "UTF-16LE");
			WriteQ(item.PlayerId);
			WriteQ(ComDiv.GetClanStatus(item.Status, item.IsOnline));
			WriteC((byte)item.Rank);
			WriteC((byte)item.NickColor);
			WriteD(item.Statistic.Clan.MatchWins);
			WriteD(item.Statistic.Clan.MatchLoses);
			WriteD(item.Equipment.NameCardId);
			WriteC((byte)item.Bonus.NickBorderColor);
			WriteD(0);
			WriteD(0);
			WriteD(0);
		}
	}
}
