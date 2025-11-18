namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_RECORD_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_RECORD_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x142b);
            base.WriteH((ushort) this.roomModel_0.FRKills);
            base.WriteH((ushort) this.roomModel_0.FRDeaths);
            base.WriteH((ushort) this.roomModel_0.FRAssists);
            base.WriteH((ushort) this.roomModel_0.CTKills);
            base.WriteH((ushort) this.roomModel_0.CTDeaths);
            base.WriteH((ushort) this.roomModel_0.CTAssists);
            foreach (SlotModel model in this.roomModel_0.Slots)
            {
                base.WriteH((ushort) model.AllKills);
                base.WriteH((ushort) model.AllDeaths);
                base.WriteH((ushort) model.AllAssists);
            }
        }
    }
}

