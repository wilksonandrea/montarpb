namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly List<int> list_0;

        public PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(RoomModel roomModel_1, List<int> list_1)
        {
            this.roomModel_0 = roomModel_1;
            this.list_0 = list_1;
        }

        private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if (roomModel_1.IsBotMode())
                {
                    packet.WriteB(Bitwise.HexStringToByteArray("FF FF FF FF FF FF FF FF FF FF"));
                }
                else if (!roomModel_1.IsDinoMode(""))
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

        private byte[] method_1(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                SlotModel[] slots = roomModel_1.Slots;
                int index = 0;
                while (true)
                {
                    if (index >= slots.Length)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    SlotModel model = slots[index];
                    PlayerEquipment equipment = model.Equipment;
                    if (equipment != null)
                    {
                        if (model.Team == TeamEnum.FR_TEAM)
                        {
                            packet.WriteD(!roomModel_1.SwapRound ? equipment.CharaRedId : equipment.CharaBlueId);
                        }
                        else if (model.Team == TeamEnum.CT_TEAM)
                        {
                            packet.WriteD(!roomModel_1.SwapRound ? equipment.CharaBlueId : equipment.CharaRedId);
                        }
                    }
                    else if (model.Team == TeamEnum.FR_TEAM)
                    {
                        packet.WriteD(!roomModel_1.SwapRound ? 0x92ba9 : 0x92f92);
                    }
                    else if (model.Team == TeamEnum.CT_TEAM)
                    {
                        packet.WriteD(!roomModel_1.SwapRound ? 0x92f92 : 0x92ba9);
                    }
                    index++;
                }
            }
            return buffer;
        }

        public override void Write()
        {
            base.WriteH((short) 0x141f);
            base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, false, false));
            base.WriteB(this.method_0(this.roomModel_0, this.list_0));
            this.WriteC(this.roomModel_0.SwapRound ? ((byte) 3) : ((byte) 0));
            if (this.roomModel_0.SwapRound)
            {
                base.WriteB(this.method_1(this.roomModel_0));
            }
        }
    }
}

