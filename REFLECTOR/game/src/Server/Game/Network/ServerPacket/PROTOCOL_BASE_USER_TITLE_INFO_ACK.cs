namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_USER_TITLE_INFO_ACK : GameServerPacket
    {
        private readonly Account account_0;

        public PROTOCOL_BASE_USER_TITLE_INFO_ACK(Account account_1)
        {
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x94f);
            base.WriteB(BitConverter.GetBytes(this.account_0.PlayerId), 0, 4);
            base.WriteQ(this.account_0.Title.Flags);
            base.WriteC((byte) this.account_0.Title.Equiped1);
            base.WriteC((byte) this.account_0.Title.Equiped2);
            base.WriteC((byte) this.account_0.Title.Equiped3);
            base.WriteD(this.account_0.Title.Slots);
        }
    }
}

