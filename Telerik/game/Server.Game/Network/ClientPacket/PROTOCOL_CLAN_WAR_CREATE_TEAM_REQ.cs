using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ : GameClientPacket
	{
		private int int_0;

		private List<int> list_0 = new List<int>();

		public PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ()
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
					ChannelModel channel = player.GetChannel();
					if (channel == null || channel.Type != ChannelType.Clan || player.Room != null)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(-2147483648, null));
					}
					else if (player.Match != null)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(-2147479417, null));
						return;
					}
					else if (player.ClanId != 0)
					{
						int ınt32 = -1;
						int ınt321 = -1;
						lock (channel.Matches)
						{
							int ınt322 = 0;
							while (true)
							{
								if (ınt322 >= 250)
								{
									break;
								}
								else if (channel.GetMatch(ınt322) == null)
								{
									ınt32 = ınt322;
									break;
								}
								else
								{
									ınt322++;
								}
							}
							for (int i = 0; i < channel.Matches.Count; i++)
							{
								MatchModel ıtem = channel.Matches[i];
								if (ıtem.Clan.Id == player.ClanId)
								{
									this.list_0.Add(ıtem.FriendId);
								}
							}
						}
						int ınt323 = 0;
						while (true)
						{
							if (ınt323 >= 25)
							{
								break;
							}
							else if (!this.list_0.Contains(ınt323))
							{
								ınt321 = ınt323;
								break;
							}
							else
							{
								ınt323++;
							}
						}
						if (ınt32 == -1)
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(-2147479416, null));
						}
						else if (ınt321 != -1)
						{
							MatchModel matchModel = new MatchModel(ClanManager.GetClan(player.ClanId))
							{
								MatchId = ınt32,
								FriendId = ınt321,
								Training = this.int_0,
								ChannelId = player.ChannelId,
								ServerId = player.ServerId
							};
							matchModel.AddPlayer(player);
							channel.AddMatch(matchModel);
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0, matchModel));
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel));
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(-2147479415, null));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(-2147479461, null));
						return;
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