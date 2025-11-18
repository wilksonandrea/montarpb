using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadH();
			this.int_1 = base.ReadH();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Match != null)
				{
					int int1 = this.int_1 - this.int_1 / 10 * 10;
					ChannelModel channel = ChannelsXML.GetChannel(this.int_1, int1);
					if (channel == null)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(-2147483648));
					}
					else
					{
						MatchModel match = channel.GetMatch(this.int_0);
						if (match == null)
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(-2147483648));
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(0, match.Clan));
						}
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