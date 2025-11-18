namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_COMMUNITY_USER_REPORT_REQ : GameClientPacket
    {
        private uint uint_0;
        private ReportType reportType_0;
        private string string_0;
        private string string_1;

        public override void Read()
        {
            this.string_1 = base.ReadU(base.ReadC() * 2);
            this.reportType_0 = (ReportType) base.ReadC();
            this.string_0 = base.ReadU(base.ReadC() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    player.FindPlayer = this.string_0;
                    Account account2 = AccountManager.GetAccount(player.FindPlayer, 1, 0x1f);
                    if ((account2 != null) && ((player.Nickname.Length > 0) && (player.Nickname != this.string_0)))
                    {
                        PlayerReport report = player.Report;
                        if ((report == null) || (report.TicketCount == 0))
                        {
                            base.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(0x800010e8));
                            return;
                        }
                        else if (player.Rank < 7)
                        {
                            this.uint_0 = 0x800010e9;
                        }
                        else if (DaoManagerSQL.CreatePlayerReportHistory(account2.PlayerId, player.PlayerId, account2.Nickname, player.Nickname, this.reportType_0, this.string_1))
                        {
                            int num;
                            report.TicketCount = num = report.TicketCount - 1;
                            if (ComDiv.UpdateDB("player_reports", "ticket_count", num, "owner_id", player.PlayerId))
                            {
                                account2.Report.ReportedCount = num = account2.Report.ReportedCount + 1;
                                ComDiv.UpdateDB("player_reports", "reported_count", num, "owner_id", account2.PlayerId);
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

