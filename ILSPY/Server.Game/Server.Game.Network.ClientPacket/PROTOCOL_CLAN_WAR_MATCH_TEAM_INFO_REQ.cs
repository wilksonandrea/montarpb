using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	public override void Read()
	{
		int_0 = ReadH();
		int_1 = ReadH();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.Match == null)
			{
				return;
			}
			int ıd = int_1 - int_1 / 10 * 10;
			ChannelModel channel = ChannelsXML.GetChannel(int_1, ıd);
			if (channel != null)
			{
				MatchModel match = channel.GetMatch(int_0);
				if (match != null)
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(0u, match.Clan));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(2147483648u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
