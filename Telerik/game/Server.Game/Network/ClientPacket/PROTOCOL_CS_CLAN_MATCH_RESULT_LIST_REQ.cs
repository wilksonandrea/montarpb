using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ : GameClientPacket
	{
		private List<MatchModel> list_0 = new List<MatchModel>();

		private int int_0;

		public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanId > 0)
					{
						ChannelModel channel = player.GetChannel();
						if (channel != null && channel.Type == ChannelType.Clan)
						{
							lock (channel.Matches)
							{
								foreach (MatchModel match in channel.Matches)
								{
									if (match.Clan.Id != player.ClanId)
									{
										continue;
									}
									this.list_0.Add(match);
								}
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK((player.ClanId == 0 ? 91 : 0), this.list_0));
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