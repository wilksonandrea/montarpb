namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ : GameClientPacket
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
                    PlayerReport report = player.Report;
                    if (report != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK(report.TicketCount));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

