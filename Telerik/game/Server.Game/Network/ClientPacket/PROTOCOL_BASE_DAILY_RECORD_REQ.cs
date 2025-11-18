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
	public class PROTOCOL_BASE_DAILY_RECORD_REQ : GameClientPacket
	{
		public PROTOCOL_BASE_DAILY_RECORD_REQ()
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
					byte lastPlaytimeFinish = 0;
					uint lastPlaytimeValue = 0;
					PlayerEvent @event = player.Event;
					if (@event != null)
					{
						lastPlaytimeFinish = (byte)@event.LastPlaytimeFinish;
						lastPlaytimeValue = (uint)@event.LastPlaytimeValue;
					}
					StatisticDaily daily = player.Statistic.Daily;
					if (daily != null)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_DAILY_RECORD_ACK(daily, lastPlaytimeFinish, lastPlaytimeValue));
					}
				}
			}
			catch (Exception exception)
			{
				CLogger.Print("PROTOCOL_BASE_DAILY_RECORD_REQ: ", LoggerType.Error, exception);
			}
		}
	}
}