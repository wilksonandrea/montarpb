namespace Server.Game.Network.ClientPacket
{
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            base.Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
        }
    }
}

