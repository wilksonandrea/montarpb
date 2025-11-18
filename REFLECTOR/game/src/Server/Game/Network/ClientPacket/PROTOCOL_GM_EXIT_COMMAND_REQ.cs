namespace Server.Game.Network.ClientPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_GM_EXIT_COMMAND_REQ : GameClientPacket
    {
        private byte byte_0;

        public override void Read()
        {
            this.byte_0 = base.ReadC();
        }

        public override void Run()
        {
        }
    }
}

