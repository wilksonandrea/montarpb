namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x9b4);
            base.WriteC(0);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteD(0x200bcf0);
        }
    }
}

