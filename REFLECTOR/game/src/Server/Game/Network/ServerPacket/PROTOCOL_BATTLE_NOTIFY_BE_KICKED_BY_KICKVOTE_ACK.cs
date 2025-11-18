namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xd51);
        }
    }
}

