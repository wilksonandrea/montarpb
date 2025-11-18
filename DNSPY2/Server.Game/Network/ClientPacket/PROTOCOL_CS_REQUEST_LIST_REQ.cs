using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001AB RID: 427
	public class PROTOCOL_CS_REQUEST_LIST_REQ : GameClientPacket
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0000561E File Offset: 0x0000381E
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00022550 File Offset: 0x00020750
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanId == 0)
					{
						this.Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(-1));
					}
					else
					{
						List<ClanInvite> clanRequestList = DaoManagerSQL.GetClanRequestList(player.ClanId);
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							int num = 0;
							int num2;
							if (this.int_0 == 0)
							{
								num2 = 13;
							}
							else
							{
								num2 = 14;
							}
							for (int i = this.int_0 * num2; i < clanRequestList.Count; i++)
							{
								this.method_0(clanRequestList[i], syncServerPacket);
								if (++num == 13)
								{
									break;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(0, num, this.int_0, syncServerPacket.ToArray()));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_REQUEST_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00022650 File Offset: 0x00020850
		private void method_0(ClanInvite clanInvite_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteQ(clanInvite_0.PlayerId);
			Account account = AccountManager.GetAccount(clanInvite_0.PlayerId, 31);
			if (account != null)
			{
				syncServerPacket_0.WriteU(account.Nickname, 66);
				syncServerPacket_0.WriteC((byte)account.Rank);
				syncServerPacket_0.WriteC((byte)account.NickColor);
				syncServerPacket_0.WriteD(clanInvite_0.InviteDate);
				syncServerPacket_0.WriteD(account.Statistic.Basic.KillsCount);
				syncServerPacket_0.WriteD(account.Statistic.Basic.DeathsCount);
				syncServerPacket_0.WriteD(account.Statistic.Basic.Matches);
				syncServerPacket_0.WriteD(account.Statistic.Basic.MatchWins);
				syncServerPacket_0.WriteD(account.Statistic.Basic.MatchLoses);
				syncServerPacket_0.WriteN(clanInvite_0.Text, clanInvite_0.Text.Length + 2, "UTF-16LE");
			}
			syncServerPacket_0.WriteD(clanInvite_0.InviteDate);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_REQUEST_LIST_REQ()
		{
		}

		// Token: 0x04000319 RID: 793
		private int int_0;
	}
}
