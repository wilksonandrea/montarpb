namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_CREATE_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly uint uint_0;

        public PROTOCOL_ROOM_CREATE_ACK(uint uint_1, RoomModel roomModel_1)
        {
            this.uint_0 = uint_1;
            this.roomModel_0 = roomModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe09);
            this.WriteD((this.uint_0 == 0) ? ((uint) this.roomModel_0.RoomId) : this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD(this.roomModel_0.RoomId);
                base.WriteU(this.roomModel_0.Name, 0x2e);
                base.WriteC((byte) this.roomModel_0.MapId);
                base.WriteC((byte) this.roomModel_0.Rule);
                base.WriteC((byte) this.roomModel_0.Stage);
                base.WriteC((byte) this.roomModel_0.RoomType);
                base.WriteC((byte) this.roomModel_0.State);
                base.WriteC((byte) this.roomModel_0.GetCountPlayers());
                base.WriteC((byte) this.roomModel_0.GetSlotCount());
                base.WriteC((byte) this.roomModel_0.Ping);
                base.WriteH((ushort) this.roomModel_0.WeaponsFlag);
                base.WriteD(this.roomModel_0.GetFlag());
                base.WriteH((short) 0);
                base.WriteD(this.roomModel_0.NewInt);
                base.WriteH((short) 0);
                base.WriteU(this.roomModel_0.LeaderName, 0x42);
                base.WriteD(this.roomModel_0.KillTime);
                base.WriteC(this.roomModel_0.Limit);
                base.WriteC(this.roomModel_0.WatchRuleFlag);
                base.WriteH((ushort) this.roomModel_0.BalanceType);
                base.WriteB(this.roomModel_0.RandomMaps);
                base.WriteC(this.roomModel_0.CountdownIG);
                base.WriteB(this.roomModel_0.LeaderAddr);
                base.WriteC(this.roomModel_0.KillCam);
                base.WriteH((short) 0);
            }
        }
    }
}

