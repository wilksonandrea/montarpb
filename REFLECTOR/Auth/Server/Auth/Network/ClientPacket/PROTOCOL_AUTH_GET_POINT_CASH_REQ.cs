namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : AuthClientPacket
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
                    base.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_GET_POINT_CASH_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

