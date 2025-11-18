namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xe63);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
        }
    }
}

