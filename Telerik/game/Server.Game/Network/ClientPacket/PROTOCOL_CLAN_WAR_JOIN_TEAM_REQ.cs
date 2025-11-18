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
	public class PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private int int_2;

		private uint uint_0;

		public PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ()
		{
		}

		private void method_0(Account account_0, MatchModel matchModel_0)
		{
			if (!matchModel_0.AddPlayer(account_0))
			{
				this.uint_0 = -2147483648;
			}
			this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(this.uint_0, matchModel_0));
			if (this.uint_0 == 0)
			{
				using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK pROTOCOLCLANWARREGISTMERCENARYACK = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel_0))
				{
					matchModel_0.SendPacketToPlayers(pROTOCOLCLANWARREGISTMERCENARYACK);
				}
			}
		}

		public override void Read()
		{
			this.int_0 = base.ReadH();
			this.int_1 = base.ReadH();
			this.int_2 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (this.int_2 >= 2 || player == null || player.Match != null || player.Room != null)
				{
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(-2147483648, null));
				}
				else
				{
					int int1 = this.int_1 - this.int_1 / 10 * 10;
					ChannelModel channel = ChannelsXML.GetChannel(this.int_1, (this.int_2 == 0 ? int1 : player.ChannelId));
					if (channel == null)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(-2147483648, null));
					}
					else if (player.ClanId != 0)
					{
						MatchModel matchModel = (this.int_2 == 1 ? channel.GetMatch(this.int_0, player.ClanId) : channel.GetMatch(this.int_0));
						if (matchModel == null)
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(-2147483648, null));
						}
						else
						{
							this.method_0(player, matchModel);
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(-2147479461, null));
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