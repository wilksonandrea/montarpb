namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_MATCH_SERVER_IDX_REQ : GameClientPacket
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
                base.Client.Close(0, false, false);
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MATCH_SERVER_IDX_REQ: " + exception.Message, LoggerType.Warning, null);
            }
        }
    }
}

