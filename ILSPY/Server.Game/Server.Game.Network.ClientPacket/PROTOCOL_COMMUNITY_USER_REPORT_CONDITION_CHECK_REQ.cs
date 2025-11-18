using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				PlayerReport report = player.Report;
				if (report != null)
				{
					Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK(report.TicketCount));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
