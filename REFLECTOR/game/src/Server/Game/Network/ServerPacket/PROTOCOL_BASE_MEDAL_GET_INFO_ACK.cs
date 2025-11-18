namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_MEDAL_GET_INFO_ACK : GameServerPacket
    {
        private readonly Account account_0;

        public PROTOCOL_BASE_MEDAL_GET_INFO_ACK(Account account_1)
        {
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x93b);
            base.WriteQ(this.account_0.PlayerId);
            base.WriteD(this.account_0.Ribbon);
            base.WriteD(this.account_0.Ensign);
            base.WriteD(this.account_0.Medal);
            base.WriteD(this.account_0.MasterMedal);
        }
    }
}

