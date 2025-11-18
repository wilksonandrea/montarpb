using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ : GameClientPacket
	{
		public PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerReport report = player.Report;
					if (report != null)
					{
						this.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK(report.TicketCount));
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