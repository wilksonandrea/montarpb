namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_ROUND_START_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        private byte[] method_0(RoomModel roomModel_1)
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
            base.WriteH((short) 0x1421);
            base.WriteC((byte) this.roomModel_0.Rounds);
            base.WriteD(this.roomModel_0.GetInBattleTimeLeft());
            base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, true, false));
            this.WriteC(this.roomModel_0.SwapRound ? ((byte) 3) : ((byte) 0));
            base.WriteH((ushort) this.roomModel_0.FRRounds);
            base.WriteH((ushort) this.roomModel_0.CTRounds);
            if (this.roomModel_0.SwapRound)
            {
                base.WriteB(this.method_0(this.roomModel_0));
            }
        }
    }
}

