namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_PRESTARTBATTLE_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly RoomModel roomModel_0;
        private readonly bool bool_0;
        private readonly bool bool_1;
        private readonly uint uint_0;
        private readonly uint uint_1;

        public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK()
        {
        }

        public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(Account account_1, bool bool_2)
        {
            this.account_0 = account_1;
            this.bool_1 = bool_2;
            this.roomModel_0 = account_1.Room;
            if (this.roomModel_0 != null)
            {
                this.bool_0 = this.roomModel_0.IsPreparing();
                this.uint_0 = this.roomModel_0.UniqueRoomId;
                this.uint_1 = this.roomModel_0.Seed;
            }
        }

        private byte[] method_0(bool bool_2)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if (bool_2)
                {
                    packet.WriteB(Bitwise.HexStringToByteArray("02 14 03 15 04 16 05 17 06 18 07 19 08 1A 09 1B0A 1C 0B 1D 0C 1E 0D 1F 0E 20 0F 21 10 22 11 0012 01 13"));
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x140a);
            base.WriteD((int) this.bool_0);
            if (this.bool_0)
            {
                base.WriteD(this.account_0.SlotId);
                this.WriteC((this.roomModel_0.RoomType == RoomCondition.Tutorial) ? ((byte) 1) : ((byte) ConfigLoader.UdpType));
                base.WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
                base.WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
                base.WriteH((ushort) ConfigLoader.DEFAULT_PORT[2]);
                base.WriteD(this.uint_0);
                base.WriteD(this.uint_1);
                base.WriteB(this.method_0(this.bool_1));
            }
        }
    }
}

