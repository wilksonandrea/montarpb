namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;
    using System.Runtime.InteropServices;

    public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK : GameServerPacket
    {
        private readonly long long_0;
        private readonly uint uint_0;

        public PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(uint uint_1, long long_1 = 0L)
        {
            this.uint_0 = uint_1;
            if (uint_1 == 1)
            {
                this.long_0 = long_1;
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x420);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 1)
            {
                base.WriteD((int) this.long_0);
            }
        }
    }
}

