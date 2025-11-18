namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_INVITED_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly RoomModel roomModel_0;

        public PROTOCOL_SERVER_MESSAGE_INVITED_ACK(Account account_1, RoomModel roomModel_1)
        {
            this.account_0 = account_1;
            this.roomModel_0 = roomModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xc05);
            base.WriteU(this.account_0.Nickname, 0x42);
            base.WriteD(this.roomModel_0.RoomId);
            base.WriteQ(this.account_0.PlayerId);
            base.WriteS(this.roomModel_0.Password, 4);
        }
    }
}

