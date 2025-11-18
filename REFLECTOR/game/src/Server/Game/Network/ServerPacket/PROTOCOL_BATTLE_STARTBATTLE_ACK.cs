namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BATTLE_STARTBATTLE_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly SlotModel slotModel_0;
        private readonly bool bool_0;
        private readonly List<int> list_0;

        public PROTOCOL_BATTLE_STARTBATTLE_ACK()
        {
        }

        public PROTOCOL_BATTLE_STARTBATTLE_ACK(SlotModel slotModel_1, Account account_0, List<int> list_1, bool bool_1)
        {
            this.slotModel_0 = slotModel_1;
            this.roomModel_0 = account_0.Room;
            if (this.roomModel_0 != null)
            {
                this.bool_0 = bool_1;
                this.list_0 = list_1;
                if (!this.roomModel_0.IsBotMode() && (this.roomModel_0.RoomType != RoomCondition.Tutorial))
                {
                    AllUtils.CompleteMission(this.roomModel_0, account_0, slotModel_1, bool_1 ? MissionType.STAGE_ENTER : MissionType.STAGE_INTERCEPT, 0);
                }
            }
        }

        private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if (!roomModel_1.IsDinoMode(""))
                {
                    packet.WriteB(new byte[10]);
                }
                else
                {
                    int num = ((list_1.Count == 1) || roomModel_1.IsDinoMode("CC")) ? 0xff : roomModel_1.TRex;
                    packet.WriteC((byte) num);
                    packet.WriteC(10);
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= list_1.Count)
                        {
                            int num2 = (8 - list_1.Count) - (num == 0xff);
                            int num5 = 0;
                            while (true)
                            {
                                if (num5 >= num2)
                                {
                                    packet.WriteC(0xff);
                                    break;
                                }
                                packet.WriteC(0xff);
                                num5++;
                            }
                            break;
                        }
                        int num4 = list_1[num3];
                        if (((num4 != roomModel_1.TRex) && roomModel_1.IsDinoMode("DE")) || roomModel_1.IsDinoMode("CC"))
                        {
                            packet.WriteC((byte) num4);
                        }
                        num3++;
                    }
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x140c);
            base.WriteH((short) 0);
            base.WriteD(0);
            base.WriteC(0);
            base.WriteB(this.method_0(this.roomModel_0, this.list_0));
            base.WriteC((byte) this.roomModel_0.Rounds);
            base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, true, false));
            this.WriteC((this.roomModel_0.ThisModeHaveRounds() || (this.roomModel_0.IsDinoMode("") || (this.roomModel_0.RoomType == RoomCondition.FreeForAll))) ? ((byte) 2) : ((byte) 0));
            if (this.roomModel_0.ThisModeHaveRounds() || (this.roomModel_0.IsDinoMode("") || (this.roomModel_0.RoomType == RoomCondition.FreeForAll)))
            {
                this.WriteH(this.roomModel_0.IsDinoMode("DE") ? ((ushort) this.roomModel_0.FRDino) : (this.roomModel_0.IsDinoMode("CC") ? ((ushort) this.roomModel_0.FRKills) : ((ushort) this.roomModel_0.FRRounds)));
                this.WriteH(this.roomModel_0.IsDinoMode("DE") ? ((ushort) this.roomModel_0.CTDino) : (this.roomModel_0.IsDinoMode("CC") ? ((ushort) this.roomModel_0.CTKills) : ((ushort) this.roomModel_0.CTRounds)));
            }
            this.WriteC((this.roomModel_0.ThisModeHaveRounds() || (this.roomModel_0.IsDinoMode("") || (this.roomModel_0.RoomType == RoomCondition.FreeForAll))) ? ((byte) 2) : ((byte) 0));
            if (this.roomModel_0.ThisModeHaveRounds() || (this.roomModel_0.IsDinoMode("") || (this.roomModel_0.RoomType == RoomCondition.FreeForAll)))
            {
                base.WriteH((short) 0);
                base.WriteH((short) 0);
            }
            base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, false, false));
            base.WriteC((byte) !this.bool_0);
            base.WriteC((byte) this.slotModel_0.Id);
        }
    }
}

