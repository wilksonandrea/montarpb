using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private int int_2;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadH();
		int_1 = ReadH();
		int_2 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (int_2 < 2 && player != null && player.Match == null && player.Room == null)
			{
				int num = int_1 - int_1 / 10 * 10;
				ChannelModel channel = ChannelsXML.GetChannel(int_1, (int_2 == 0) ? num : player.ChannelId);
				if (channel != null)
				{
					if (player.ClanId == 0)
					{
						Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147487835u));
						return;
					}
					MatchModel matchModel = ((int_2 == 1) ? channel.GetMatch(int_0, player.ClanId) : channel.GetMatch(int_0));
					if (matchModel != null)
					{
						method_0(player, matchModel);
					}
					else
					{
						Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648u));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(Account account_0, MatchModel matchModel_0)
	{
		if (!matchModel_0.AddPlayer(account_0))
		{
			uint_0 = 2147483648u;
		}
		Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(uint_0, matchModel_0));
		if (uint_0 == 0)
		{
			using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK packet = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel_0))
			{
				matchModel_0.SendPacketToPlayers(packet);
			}
		}
	}
}
