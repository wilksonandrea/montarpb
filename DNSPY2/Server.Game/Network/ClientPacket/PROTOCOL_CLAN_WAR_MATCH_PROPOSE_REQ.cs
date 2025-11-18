using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000183 RID: 387
	public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ : GameClientPacket
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x00005343 File Offset: 0x00003543
		public override void Read()
		{
			this.int_0 = (int)base.ReadH();
			this.int_1 = (int)base.ReadH();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Match != null && player.MatchSlot == player.Match.Leader && player.Match.State == MatchState.Ready)
					{
						int num = this.int_1 - this.int_1 / 10 * 10;
						MatchModel match = ChannelsXML.GetChannel(this.int_1, num).GetMatch(this.int_0);
						if (match != null)
						{
							Account leader = match.GetLeader();
							if (leader != null && leader.Connection != null && leader.IsOnline)
							{
								leader.SendPacket(new PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(player.Match, player));
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else
						{
							this.uint_0 = 2147483648U;
						}
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ()
		{
		}

		// Token: 0x040002D9 RID: 729
		private int int_0;

		// Token: 0x040002DA RID: 730
		private int int_1;

		// Token: 0x040002DB RID: 731
		private uint uint_0;
	}
}
