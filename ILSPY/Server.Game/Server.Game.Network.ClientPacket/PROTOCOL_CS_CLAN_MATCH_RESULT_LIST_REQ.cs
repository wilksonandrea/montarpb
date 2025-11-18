using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ : GameClientPacket
{
	private List<MatchModel> list_0 = new List<MatchModel>();

	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (player.ClanId > 0)
			{
				ChannelModel channel = player.GetChannel();
				if (channel != null && channel.Type == ChannelType.Clan)
				{
					lock (channel.Matches)
					{
						foreach (MatchModel match in channel.Matches)
						{
							if (match.Clan.Id == player.ClanId)
							{
								list_0.Add(match);
							}
						}
					}
				}
			}
			Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK((player.ClanId == 0) ? 91 : 0, list_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
