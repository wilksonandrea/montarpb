namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1435);
            base.WriteC(this.roomModel_0.IngameAiLevel);
            for (int i = 0; i < 0x12; i++)
            {
                base.WriteD(this.roomModel_0.Slots[i].AiLevel);
            }
        }
    }
}

