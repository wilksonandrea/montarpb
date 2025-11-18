using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x0200003A RID: 58
	public class PROTOCOL_BASE_DAILY_RECORD_REQ : AuthClientPacket
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000072E0 File Offset: 0x000054E0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					byte b = 0;
					uint num = 0U;
					PlayerEvent @event = player.Event;
					if (@event != null)
					{
						b = (byte)@event.LastPlaytimeFinish;
						num = (uint)@event.LastPlaytimeValue;
					}
					StatisticDaily daily = player.Statistic.Daily;
					if (daily != null)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_DAILY_RECORD_ACK(daily, b, num));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_DAILY_RECORD_REQ: ", LoggerType.Error, ex);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_DAILY_RECORD_REQ()
		{
		}
	}
}
