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
	public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private uint uint_0;

		public PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ()
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
				if (player != null)
				{
					if (player.Match == null || player.MatchSlot != player.Match.Leader || player.Match.State != MatchState.Ready)
					{
						this.uint_0 = -2147483648;
					}
					else
					{
						int int1 = this.int_1 - this.int_1 / 10 * 10;
						MatchModel match = ChannelsXML.GetChannel(this.int_1, int1).GetMatch(this.int_0);
						if (match == null)
						{
							this.uint_0 = -2147483648;
						}
						else
						{
							Account leader = match.GetLeader();
							if (leader == null || leader.Connection == null || !leader.IsOnline)
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								leader.SendPacket(new PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(player.Match, player));
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}