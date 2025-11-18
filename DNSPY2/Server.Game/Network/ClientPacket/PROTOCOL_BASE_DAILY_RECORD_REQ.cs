using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200013B RID: 315
	public class PROTOCOL_BASE_DAILY_RECORD_REQ : GameClientPacket
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00018C0C File Offset: 0x00016E0C
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

		// Token: 0x06000311 RID: 785 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_DAILY_RECORD_REQ()
		{
		}
	}
}
