namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x393);
            base.WriteC((byte) ConfigLoader.MinCreateRank);
            base.WriteD(ConfigLoader.MinCreateGold);
        }
    }
}

