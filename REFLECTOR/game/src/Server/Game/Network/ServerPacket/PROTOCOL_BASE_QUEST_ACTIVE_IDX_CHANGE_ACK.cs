namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly uint uint_0;
        private readonly int int_0;

        public PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(uint uint_1, int int_1, Account account_1)
        {
            this.uint_0 = uint_1;
            this.int_0 = int_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x939);
            base.WriteD(this.uint_0);
            base.WriteC((byte) this.int_0);
            if ((this.uint_0 & 1) == 1)
            {
                base.WriteD(this.account_0.Exp);
                base.WriteD(this.account_0.Gold);
                base.WriteD(this.account_0.Ribbon);
                base.WriteD(this.account_0.Ensign);
                base.WriteD(this.account_0.Medal);
                base.WriteD(this.account_0.MasterMedal);
                base.WriteD(this.account_0.Rank);
            }
        }
    }
}

