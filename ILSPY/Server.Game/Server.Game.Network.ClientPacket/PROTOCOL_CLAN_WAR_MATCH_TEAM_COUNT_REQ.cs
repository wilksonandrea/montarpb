using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null && player.Match != null)
			{
				ChannelModel channel = player.GetChannel();
				if (channel != null && channel.Type == ChannelType.Clan)
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(channel.Matches.Count));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
