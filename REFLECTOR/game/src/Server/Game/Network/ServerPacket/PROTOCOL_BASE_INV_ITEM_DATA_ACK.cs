namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_INV_ITEM_DATA_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly Account account_0;

        public PROTOCOL_BASE_INV_ITEM_DATA_ACK(int int_1, Account account_1)
        {
            this.int_0 = int_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x95b);
            base.WriteC((byte) this.int_0);
            base.WriteC((byte) this.account_0.NickColor);
            base.WriteD(this.account_0.Bonus.FakeRank);
            base.WriteD(this.account_0.Bonus.FakeRank);
            base.WriteU(this.account_0.Bonus.FakeNick, 0x42);
            base.WriteH((short) this.account_0.Bonus.CrosshairColor);
            base.WriteH((short) this.account_0.Bonus.MuzzleColor);
            base.WriteC((byte) this.account_0.Bonus.NickBorderColor);
        }
    }
}

