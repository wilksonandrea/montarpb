namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_INVENTORY_LEAVE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xd04);
            base.WriteH((short) 0);
            base.WriteD(0);
        }
    }
}

