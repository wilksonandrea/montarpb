namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x2103);
            base.WriteH((short) 0);
            base.WriteC(1);
        }
    }
}

