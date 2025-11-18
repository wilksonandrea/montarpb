using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
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
						for (int i = 0; i < channel.Matches.Count; i++)
						{
							if (channel.Matches[i].Clan.Id == player.ClanId)
							{
								int_0++;
							}
						}
					}
				}
			}
			Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(int_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
