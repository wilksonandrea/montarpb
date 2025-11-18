namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_QUEST_CHANGE_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_BASE_QUEST_CHANGE_ACK(int int_2, MissionCardModel missionCardModel_0)
        {
            this.int_0 = missionCardModel_0.MissionBasicId;
            if (missionCardModel_0.MissionLimit == int_2)
            {
                this.int_0 += 240;
            }
            this.int_1 = int_2;
        }

        public override void Write()
        {
            base.WriteH((short) 0x937);
            base.WriteC((byte) this.int_0);
            base.WriteC((byte) this.int_1);
        }
    }
}

