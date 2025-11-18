namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_REPLACE_INTRO_ACK : GameServerPacket
    {
        private readonly EventErrorEnum eventErrorEnum_0;

        public PROTOCOL_CS_REPLACE_INTRO_ACK(EventErrorEnum eventErrorEnum_1)
        {
            this.eventErrorEnum_0 = eventErrorEnum_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x35d);
            base.WriteD((uint) this.eventErrorEnum_0);
        }
    }
}

