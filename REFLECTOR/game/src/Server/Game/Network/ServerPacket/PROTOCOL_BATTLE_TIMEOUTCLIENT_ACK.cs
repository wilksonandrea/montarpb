namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x1418);
            base.WriteH((short) 0);
        }
    }
}

