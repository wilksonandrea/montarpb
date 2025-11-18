using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200017E RID: 382
	public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ : GameClientPacket
	{
		// Token: 0x060003DE RID: 990 RVA: 0x000052BC File Offset: 0x000034BC
		public override void Read()
		{
			base.ReadD();
			this.int_0 = (int)base.ReadH();
			this.int_1 = (int)base.ReadH();
			this.int_2 = (int)base.ReadC();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001F04C File Offset: 0x0001D24C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					MatchModel match = player.Match;
					int num = this.int_1 - this.int_1 / 10 * 10;
					MatchModel match2 = ChannelsXML.GetChannel(this.int_1, num).GetMatch(this.int_0);
					if (match != null && match2 != null && player.MatchSlot == match.Leader)
					{
						if (this.int_2 == 1)
						{
							if (match.Training != match2.Training)
							{
								this.uint_0 = 2147487890U;
							}
							else
							{
								if (match2.GetCountPlayers() == match.Training)
								{
									if (match.GetCountPlayers() == match.Training)
									{
										if (match2.State != MatchState.Play)
										{
											if (match.State != MatchState.Play)
											{
												match.State = MatchState.Play;
												Account leader = match2.GetLeader();
												if (leader != null && leader.Match != null)
												{
													leader.SendPacket(new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match));
													leader.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(match));
													match2.Slots[leader.MatchSlot].State = SlotMatchState.Ready;
												}
												match2.State = MatchState.Play;
												goto IL_163;
											}
										}
										this.uint_0 = 2147487888U;
										goto IL_163;
									}
								}
								this.uint_0 = 2147487889U;
							}
						}
						else
						{
							Account leader2 = match2.GetLeader();
							if (leader2 != null && leader2.Match != null)
							{
								leader2.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147487891U));
							}
						}
					}
					else
					{
						this.uint_0 = 2147487892U;
					}
					IL_163:
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("CLAN_WAR_ACCEPT_BATTLE_REC: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ()
		{
		}

		// Token: 0x040002CC RID: 716
		private int int_0;

		// Token: 0x040002CD RID: 717
		private int int_1;

		// Token: 0x040002CE RID: 718
		private int int_2;

		// Token: 0x040002CF RID: 719
		private uint uint_0;
	}
}
