namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_LOGOUT_REQ : AuthClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_BASE_LOGOUT_ACK());
                base.Client.Close(0x1388, true);
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_LOGOUT_REQ: " + exception.Message, LoggerType.Error, exception);
                base.Client.Close(0, true);
            }
        }
    }
}

