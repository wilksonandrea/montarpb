namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_DAILY_RECORD_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    byte lastPlaytimeFinish = 0;
                    uint lastPlaytimeValue = 0;
                    PlayerEvent event2 = player.Event;
                    if (event2 != null)
                    {
                        lastPlaytimeFinish = (byte) event2.LastPlaytimeFinish;
                        lastPlaytimeValue = (uint) event2.LastPlaytimeValue;
                    }
                    StatisticDaily daily = player.Statistic.Daily;
                    if (daily != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_DAILY_RECORD_ACK(daily, lastPlaytimeFinish, lastPlaytimeValue));
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

