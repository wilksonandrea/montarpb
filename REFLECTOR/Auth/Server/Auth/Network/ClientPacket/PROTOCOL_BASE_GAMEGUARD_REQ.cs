namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GAMEGUARD_REQ : AuthClientPacket
    {
        private byte[] byte_0;

        public override void Read()
        {
            base.ReadB(0x30);
            this.byte_0 = base.ReadB(3);
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_BASE_GAMEGUARD_ACK(0, this.byte_0));
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_GAMEGUARD_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

