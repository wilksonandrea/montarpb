using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200015C RID: 348
	public class PROTOCOL_COMMUNITY_USER_REPORT_REQ : GameClientPacket
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000050C7 File Offset: 0x000032C7
		public override void Read()
		{
			this.string_1 = base.ReadU((int)(base.ReadC() * 2));
			this.reportType_0 = (ReportType)base.ReadC();
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001AA40 File Offset: 0x00018C40
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					player.FindPlayer = this.string_0;
					Account account = AccountManager.GetAccount(player.FindPlayer, 1, 31);
					if (account != null && player.Nickname.Length > 0 && player.Nickname != this.string_0)
					{
						PlayerReport report = player.Report;
						if (report == null || report.TicketCount == 0)
						{
							this.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(2147487976U));
							return;
						}
						if (player.Rank < 7)
						{
							this.uint_0 = 2147487977U;
						}
						else if (DaoManagerSQL.CreatePlayerReportHistory(account.PlayerId, player.PlayerId, account.Nickname, player.Nickname, this.reportType_0, this.string_1) && ComDiv.UpdateDB("player_reports", "ticket_count", --report.TicketCount, "owner_id", player.PlayerId))
						{
							ComDiv.UpdateDB("player_reports", "reported_count", ++account.Report.ReportedCount, "owner_id", account.PlayerId);
						}
					}
					this.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_COMMUNITY_USER_REPORT_REQ()
		{
		}

		// Token: 0x0400027A RID: 634
		private uint uint_0;

		// Token: 0x0400027B RID: 635
		private ReportType reportType_0;

		// Token: 0x0400027C RID: 636
		private string string_0;

		// Token: 0x0400027D RID: 637
		private string string_1;
	}
}
