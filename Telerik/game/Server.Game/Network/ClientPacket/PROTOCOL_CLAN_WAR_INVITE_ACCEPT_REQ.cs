using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private int int_2;

		private uint uint_0;

		public PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ()
		{
		}

		public override void Read()
		{
			base.ReadD();
			this.int_0 = base.ReadH();
			this.int_1 = base.ReadH();
			this.int_2 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					MatchModel match = player.Match;
					int int1 = this.int_1 - this.int_1 / 10 * 10;
					MatchModel matchModel = ChannelsXML.GetChannel(this.int_1, int1).GetMatch(this.int_0);
					if (match == null || matchModel == null || player.MatchSlot != match.Leader)
					{
						this.uint_0 = -2147479404;
					}
					else if (this.int_2 != 1)
					{
						Account leader = matchModel.GetLeader();
						if (leader != null && leader.Match != null)
						{
							leader.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(-2147479405));
						}
					}
					else if (match.Training == matchModel.Training)
					{
						if (matchModel.GetCountPlayers() == match.Training)
						{
							if (match.GetCountPlayers() != match.Training)
							{
								goto Label3;
							}
							if (matchModel.State != MatchState.Play)
							{
								if (match.State == MatchState.Play)
								{
									goto Label4;
								}
								match.State = MatchState.Play;
								Account account = matchModel.GetLeader();
								if (account != null && account.Match != null)
								{
									account.SendPacket(new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match));
									account.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(match));
									matchModel.Slots[account.MatchSlot].State = SlotMatchState.Ready;
								}
								matchModel.State = MatchState.Play;
								goto Label0;
							}
						Label4:
							this.uint_0 = -2147479408;
							goto Label0;
						}
					Label3:
						this.uint_0 = -2147479407;
					}
					else
					{
						this.uint_0 = -2147479406;
					}
				Label0:
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("CLAN_WAR_ACCEPT_BATTLE_REC: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}