namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK : GameServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1427);
            base.WriteD(this.int_0);
        }
    }
}

