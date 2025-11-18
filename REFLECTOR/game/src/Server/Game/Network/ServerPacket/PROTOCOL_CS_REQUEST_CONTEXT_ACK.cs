namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_REQUEST_CONTEXT_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly int int_0;

        public PROTOCOL_CS_REQUEST_CONTEXT_ACK(int int_1)
        {
            if (int_1 > 0)
            {
                this.int_0 = DaoManagerSQL.GetRequestClanInviteCount(int_1);
            }
            else
            {
                this.uint_0 = uint.MaxValue;
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x331);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteC((byte) this.int_0);
                base.WriteC(13);
                base.WriteC((byte) Math.Ceiling((double) (((double) this.int_0) / 13.0)));
                base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
            }
        }
    }
}

