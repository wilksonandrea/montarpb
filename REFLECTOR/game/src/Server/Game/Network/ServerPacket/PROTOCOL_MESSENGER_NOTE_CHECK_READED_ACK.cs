namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK : GameServerPacket
    {
        private readonly List<int> list_0;

        public PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(List<int> list_1)
        {
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x787);
            base.WriteC((byte) this.list_0.Count);
            for (int i = 0; i < this.list_0.Count; i++)
            {
                base.WriteD(this.list_0[i]);
            }
        }
    }
}

