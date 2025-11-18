using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ : GameClientPacket
	{
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Match != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null && channel.Type == ChannelType.Clan)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(channel.Matches.Count));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}