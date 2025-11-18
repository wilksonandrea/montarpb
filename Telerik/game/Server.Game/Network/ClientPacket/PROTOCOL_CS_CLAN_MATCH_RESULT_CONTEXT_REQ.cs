using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ()
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
				if (player != null)
				{
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
										this.int_0++;
									}
								}
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(this.int_0));
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