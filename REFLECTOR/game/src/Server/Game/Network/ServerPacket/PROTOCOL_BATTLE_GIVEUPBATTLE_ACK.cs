namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_GIVEUPBATTLE_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly int int_0;

        public PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Account account_1, int int_1)
        {
            this.account_0 = account_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x140e);
            base.WriteD(this.account_0.SlotId);
            base.WriteC((byte) this.int_0);
            base.WriteD(this.account_0.Exp);
            base.WriteD(this.account_0.Rank);
            base.WriteD(this.account_0.Gold);
            base.WriteD(this.account_0.Statistic.Season.EscapesCount);
            base.WriteD(this.account_0.Statistic.Basic.EscapesCount);
            base.WriteD(0);
            base.WriteC(0);
        }
    }
}

