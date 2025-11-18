namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        public int GetSlotKill()
        {
            int[] numArray = new int[0x12];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = this.roomModel_0.Slots[i].AllKills;
            }
            int index = 0;
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] > numArray[index])
                {
                    index = j;
                }
            }
            return index;
        }

        private byte[] method_0(RoomModel roomModel_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if (roomModel_1.IsDinoMode("DE"))
                {
                    packet.WriteD(roomModel_1.FRDino);
                    packet.WriteD(roomModel_1.CTDino);
                }
                else if ((roomModel_1.RoomType == RoomCondition.DeathMatch) && !roomModel_1.IsBotMode())
                {
                    packet.WriteD(roomModel_1.FRKills);
                    packet.WriteD(roomModel_1.CTKills);
                }
                else if (roomModel_1.RoomType == RoomCondition.FreeForAll)
                {
                    packet.WriteD(this.GetSlotKill());
                    packet.WriteD(0);
                }
                else if (roomModel_1.IsBotMode())
                {
                    packet.WriteD((int) roomModel_1.IngameAiLevel);
                    packet.WriteD(0);
                }
                else
                {
                    packet.WriteD(roomModel_1.FRRounds);
                    packet.WriteD(roomModel_1.CTRounds);
                }
                return packet.ToArray();
            }
        }

        private int method_1() => 
            !this.roomModel_0.IsBotMode() ? (((this.roomModel_0.RoomType != RoomCondition.DeathMatch) || this.roomModel_0.IsBotMode()) ? (!this.roomModel_0.IsDinoMode("") ? ((this.roomModel_0.RoomType != RoomCondition.FreeForAll) ? 2 : 5) : 4) : 1) : 3;

        public override void Write()
        {
            base.WriteH((short) 0x148f);
            base.WriteD(this.method_1());
            base.WriteD((int) ((this.roomModel_0.GetTimeByMask() * 60) - this.roomModel_0.GetInBattleTime()));
            base.WriteB(this.method_0(this.roomModel_0));
        }
    }
}

