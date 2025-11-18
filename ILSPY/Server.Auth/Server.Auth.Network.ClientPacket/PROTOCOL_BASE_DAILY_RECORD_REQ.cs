using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_DAILY_RECORD_REQ : AuthClientPacket
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
				byte byte_ = 0;
				uint uint_ = 0u;
				PlayerEvent @event = player.Event;
				if (@event != null)
				{
					byte_ = (byte)@event.LastPlaytimeFinish;
					uint_ = (uint)@event.LastPlaytimeValue;
				}
				StatisticDaily daily = player.Statistic.Daily;
				if (daily != null)
				{
					Client.SendPacket(new PROTOCOL_BASE_DAILY_RECORD_ACK(daily, byte_, uint_));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_DAILY_RECORD_REQ: ", LoggerType.Error, ex);
		}
	}
}
