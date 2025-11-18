namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_CHANGE_PASSWD_ACK : GameServerPacket
    {
        private readonly string string_0;

        public PROTOCOL_ROOM_CHANGE_PASSWD_ACK(string string_1)
        {
            this.string_0 = string_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe13);
            base.WriteS(this.string_0, 4);
        }
    }
}

