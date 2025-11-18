namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x142f);
            base.WriteH((ushort) this.roomModel_0.Bar1);
            base.WriteH((ushort) this.roomModel_0.Bar2);
            for (int i = 0; i < 0x12; i++)
            {
                base.WriteH(this.roomModel_0.Slots[i].DamageBar1);
            }
        }
    }
}

