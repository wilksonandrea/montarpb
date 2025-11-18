using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_COMMUNITY_USER_REPORT_REQ : GameClientPacket
	{
		private uint uint_0;

		private ReportType reportType_0;

		private string string_0;

		private string string_1;

		public PROTOCOL_COMMUNITY_USER_REPORT_REQ()
		{
		}

		public override void Read()
		{
			this.string_1 = base.ReadU(base.ReadC() * 2);
			this.reportType_0 = (ReportType)base.ReadC();
			this.string_0 = base.ReadU(base.ReadC() * 2);
		}

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
							this.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(-2147479320));
							return;
						}
						else if (player.Rank < 7)
						{
							this.uint_0 = -2147479319;
						}
						else if (DaoManagerSQL.CreatePlayerReportHistory(account.PlayerId, player.PlayerId, account.Nickname, player.Nickname, this.reportType_0, this.string_1))
						{
							PlayerReport playerReport = report;
							int ticketCount = playerReport.TicketCount - 1;
							int ınt32 = ticketCount;
							playerReport.TicketCount = ticketCount;
							if (ComDiv.UpdateDB("player_reports", "ticket_count", ınt32, "owner_id", player.PlayerId))
							{
								PlayerReport report1 = account.Report;
								int reportedCount = report1.ReportedCount + 1;
								ınt32 = reportedCount;
								report1.ReportedCount = reportedCount;
								ComDiv.UpdateDB("player_reports", "reported_count", ınt32, "owner_id", account.PlayerId);
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(this.uint_0));
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