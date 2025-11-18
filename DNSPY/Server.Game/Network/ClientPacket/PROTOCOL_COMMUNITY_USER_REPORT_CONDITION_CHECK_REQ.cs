using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200015D RID: 349
	public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ : GameClientPacket
	{
		// Token: 0x06000377 RID: 887 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001ABDC File Offset: 0x00018DDC
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
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ()
		{
		}
	}
}
