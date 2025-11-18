using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200017D RID: 381
	public class PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ : GameClientPacket
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0000529B File Offset: 0x0000349B
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null && channel.Type == ChannelType.Clan && player.Room == null)
					{
						if (player.Match != null)
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487879U, null));
						}
						else
						{
							if (player.ClanId != 0)
							{
								int num = -1;
								int num2 = -1;
								List<MatchModel> matches = channel.Matches;
								lock (matches)
								{
									for (int i = 0; i < 250; i++)
									{
										if (channel.GetMatch(i) == null)
										{
											num = i;
											IL_B9:
											for (int j = 0; j < channel.Matches.Count; j++)
											{
												MatchModel matchModel = channel.Matches[j];
												if (matchModel.Clan.Id == player.ClanId)
												{
													this.list_0.Add(matchModel.FriendId);
												}
											}
											goto IL_116;
										}
									}
									goto IL_B9;
								}
								IL_116:
								int k = 0;
								while (k < 25)
								{
									if (this.list_0.Contains(k))
									{
										k++;
									}
									else
									{
										num2 = k;
										IL_13B:
										if (num == -1)
										{
											this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487880U, null));
											goto IL_20B;
										}
										if (num2 == -1)
										{
											this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487881U, null));
											goto IL_20B;
										}
										MatchModel matchModel2 = new MatchModel(ClanManager.GetClan(player.ClanId))
										{
											MatchId = num,
											FriendId = num2,
											Training = this.int_0,
											ChannelId = player.ChannelId,
											ServerId = player.ServerId
										};
										matchModel2.AddPlayer(player);
										channel.AddMatch(matchModel2);
										this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0U, matchModel2));
										this.Client.SendPacket(new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel2));
										goto IL_20B;
									}
								}
								goto IL_13B;
							}
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487835U, null));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147483648U, null));
					}
					IL_20B:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000052A9 File Offset: 0x000034A9
		public PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ()
		{
		}

		// Token: 0x040002CA RID: 714
		private int int_0;

		// Token: 0x040002CB RID: 715
		private List<int> list_0 = new List<int>();
	}
}
