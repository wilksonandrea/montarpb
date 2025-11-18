namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_MESSENGER_NOTE_DELETE_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly List<object> list_0;

        public PROTOCOL_MESSENGER_NOTE_DELETE_ACK(uint uint_1, List<object> list_1)
        {
            this.uint_0 = uint_1;
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x789);
            base.WriteD(this.uint_0);
            base.WriteC((byte) this.list_0.Count);
            foreach (long num in this.list_0)
            {
                base.WriteD((uint) num);
            }
        }
    }
}

