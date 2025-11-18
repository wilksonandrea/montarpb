namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_ROOM_TEAM_BALANCE_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly List<SlotChange> list_0;

        public PROTOCOL_ROOM_TEAM_BALANCE_ACK(List<SlotChange> list_1, int int_2, int int_3)
        {
            this.list_0 = list_1;
            this.int_1 = int_2;
            this.int_0 = int_3;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe26);
            base.WriteC((byte) this.int_0);
            base.WriteC((byte) this.int_1);
            base.WriteC((byte) this.list_0.Count);
            foreach (SlotChange change in this.list_0)
            {
                base.WriteC((byte) change.OldSlot.Id);
                base.WriteC((byte) change.NewSlot.Id);
                base.WriteC((byte) change.OldSlot.State);
                base.WriteC((byte) change.NewSlot.State);
            }
        }
    }
}

