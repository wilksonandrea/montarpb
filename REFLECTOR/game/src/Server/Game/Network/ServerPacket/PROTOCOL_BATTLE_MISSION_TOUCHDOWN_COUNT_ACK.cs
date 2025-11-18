namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1441);
            base.WriteH((ushort) this.roomModel_0.FRDino);
            base.WriteH((ushort) this.roomModel_0.CTDino);
        }
    }
}

