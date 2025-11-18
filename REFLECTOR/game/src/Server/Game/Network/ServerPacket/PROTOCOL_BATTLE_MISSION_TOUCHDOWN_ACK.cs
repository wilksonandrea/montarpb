namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly SlotModel slotModel_0;

        public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(RoomModel roomModel_1, SlotModel slotModel_1)
        {
            this.roomModel_0 = roomModel_1;
            this.slotModel_0 = slotModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x143b);
            base.WriteH((ushort) this.roomModel_0.FRDino);
            base.WriteH((ushort) this.roomModel_0.CTDino);
            base.WriteD(this.slotModel_0.Id);
            base.WriteH((short) this.slotModel_0.PassSequence);
        }
    }
}

