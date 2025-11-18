namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_ROUND_END_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly int int_0;
        private readonly RoundEndType roundEndType_0;

        public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(RoomModel roomModel_1, TeamEnum teamEnum_0, RoundEndType roundEndType_1)
        {
            this.roomModel_0 = roomModel_1;
            this.int_0 = (int) teamEnum_0;
            this.roundEndType_0 = roundEndType_1;
        }

        public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(RoomModel roomModel_1, int int_1, RoundEndType roundEndType_1)
        {
            this.roomModel_0 = roomModel_1;
            this.int_0 = int_1;
            this.roundEndType_0 = roundEndType_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1423);
            base.WriteC((byte) this.int_0);
            base.WriteC((byte) this.roundEndType_0);
            if (this.roomModel_0.IsDinoMode("DE"))
            {
                base.WriteH((ushort) this.roomModel_0.FRDino);
                base.WriteH((ushort) this.roomModel_0.CTDino);
            }
            else if ((this.roomModel_0.RoomType != RoomCondition.DeathMatch) && (this.roomModel_0.RoomType != RoomCondition.FreeForAll))
            {
                base.WriteH((ushort) this.roomModel_0.FRRounds);
                base.WriteH((ushort) this.roomModel_0.CTRounds);
            }
            else
            {
                base.WriteH((ushort) this.roomModel_0.FRKills);
                base.WriteH((ushort) this.roomModel_0.CTKills);
            }
        }
    }
}

