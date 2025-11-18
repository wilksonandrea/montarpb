namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_MATCH_SERVER_IDX_REQ : AuthClientPacket
    {
        private short short_0;

        public override void Read()
        {
            this.short_0 = base.ReadH();
            base.ReadC();
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_MATCH_SERVER_IDX_ACK(this.short_0));
                base.Client.Close(0, false);
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MATCH_SERVER_IDX_REQ: " + exception.Message, LoggerType.Warning, null);
            }
        }
    }
}

