using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_REQ : GameClientPacket
{
	private uint uint_0;

	private ReportType reportType_0;

	private string string_0;

	private string string_1;

	public override void Read()
	{
		string_1 = ReadU(ReadC() * 2);
		reportType_0 = (ReportType)ReadC();
		string_0 = ReadU(ReadC() * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			player.FindPlayer = string_0;
			Account account = AccountManager.GetAccount(player.FindPlayer, 1, 31);
			if (account != null && player.Nickname.Length > 0 && player.Nickname != string_0)
			{
				PlayerReport report = player.Report;
				if (report == null || report.TicketCount == 0)
				{
					Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(2147487976u));
					return;
				}
				if (player.Rank < 7)
				{
					uint_0 = 2147487977u;
				}
				else if (DaoManagerSQL.CreatePlayerReportHistory(account.PlayerId, player.PlayerId, account.Nickname, player.Nickname, reportType_0, string_1) && ComDiv.UpdateDB("player_reports", "ticket_count", --report.TicketCount, "owner_id", player.PlayerId))
				{
					ComDiv.UpdateDB("player_reports", "reported_count", ++account.Report.ReportedCount, "owner_id", account.PlayerId);
				}
			}
			Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
