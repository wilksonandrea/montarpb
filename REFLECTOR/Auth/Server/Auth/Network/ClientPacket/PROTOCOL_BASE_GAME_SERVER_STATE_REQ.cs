namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GAME_SERVER_STATE_REQ : AuthClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_BASE_GAME_SERVER_STATE_ACK());
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

