namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_AUTH_GET_POINT_CASH_ACK : AuthServerPacket
    {
        private readonly uint uint_0;
        private readonly Account account_0;

        public PROTOCOL_AUTH_GET_POINT_CASH_ACK(uint uint_1, Account account_1)
        {
            this.uint_0 = uint_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x422);
            base.WriteD(this.uint_0);
            base.WriteD(this.account_0.Gold);
            base.WriteD(this.account_0.Cash);
            base.WriteD(this.account_0.Tags);
            base.WriteD(0);
        }
    }
}

