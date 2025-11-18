namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x2103);
            base.WriteH((short) 0);
            base.WriteC(1);
        }
    }
}

