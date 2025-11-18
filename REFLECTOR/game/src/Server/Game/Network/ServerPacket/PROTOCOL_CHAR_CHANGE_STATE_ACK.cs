namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CHAR_CHANGE_STATE_ACK : GameServerPacket
    {
        private readonly CharacterModel characterModel_0;

        public PROTOCOL_CHAR_CHANGE_STATE_ACK(CharacterModel characterModel_1)
        {
            this.characterModel_0 = characterModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1809);
            base.WriteH((short) 0);
            base.WriteD(0);
            base.WriteC(20);
            base.WriteC((byte) this.characterModel_0.Slot);
        }
    }
}

