namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_START_GAME_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_BATTLE_START_GAME_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
        }

        private byte[] method_0(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) roomModel_1.GetReadyPlayers());
                SlotModel[] slots = roomModel_1.Slots;
                int index = 0;
                while (true)
                {
                    if (index >= slots.Length)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    SlotModel slot = slots[index];
                    if ((slot.State >= SlotState.READY) && (slot.Equipment != null))
                    {
                        Account playerBySlot = roomModel_1.GetPlayerBySlot(slot);
                        if ((playerBySlot != null) && (playerBySlot.SlotId == slot.Id))
                        {
                            packet.WriteD((uint) playerBySlot.PlayerId);
                        }
                    }
                    index++;
                }
            }
            return buffer;
        }

        private byte[] method_1(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) roomModel_1.Slots.Length);
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
                    packet.WriteC((byte) model.Team);
                    index++;
                }
            }
            return buffer;
        }

        private byte[] method_2(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) roomModel_1.GetReadyPlayers());
                SlotModel[] slots = roomModel_1.Slots;
                int index = 0;
                while (true)
                {
                    if (index >= slots.Length)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    SlotModel slot = slots[index];
                    if ((slot.State >= SlotState.READY) && (slot.Equipment != null))
                    {
                        Account playerBySlot = roomModel_1.GetPlayerBySlot(slot);
                        if ((playerBySlot != null) && (playerBySlot.SlotId == slot.Id))
                        {
                            packet.WriteC((byte) slot.Id);
                            PlayerEquipment equipment = playerBySlot.Equipment;
                            PlayerTitles title = playerBySlot.Title;
                            int charaRedId = 0;
                            if ((equipment != null) && (title != null))
                            {
                                TeamEnum enum2 = roomModel_1.ValidateTeam(slot.Team, slot.CostumeTeam);
                                if (enum2 == TeamEnum.FR_TEAM)
                                {
                                    charaRedId = equipment.CharaRedId;
                                }
                                else if (enum2 == TeamEnum.CT_TEAM)
                                {
                                    charaRedId = equipment.CharaBlueId;
                                }
                                packet.WriteD(charaRedId);
                                packet.WriteD(equipment.WeaponPrimary);
                                packet.WriteD(equipment.WeaponSecondary);
                                packet.WriteD(equipment.WeaponMelee);
                                packet.WriteD(equipment.WeaponExplosive);
                                packet.WriteD(equipment.WeaponSpecial);
                                packet.WriteD(charaRedId);
                                packet.WriteD(equipment.PartHead);
                                packet.WriteD(equipment.PartFace);
                                packet.WriteD(equipment.PartJacket);
                                packet.WriteD(equipment.PartPocket);
                                packet.WriteD(equipment.PartGlove);
                                packet.WriteD(equipment.PartBelt);
                                packet.WriteD(equipment.PartHolster);
                                packet.WriteD(equipment.PartSkin);
                                packet.WriteD(equipment.BeretItem);
                                packet.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
                                packet.WriteC((byte) title.Equiped1);
                                packet.WriteC((byte) title.Equiped2);
                                packet.WriteC((byte) title.Equiped3);
                                packet.WriteD(equipment.AccessoryId);
                                packet.WriteD(equipment.SprayId);
                                packet.WriteD(equipment.NameCardId);
                            }
                        }
                    }
                    index++;
                }
            }
            return buffer;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1407);
            base.WriteH((short) 0);
            base.WriteB(this.method_0(this.roomModel_0));
            base.WriteB(this.method_1(this.roomModel_0));
            base.WriteB(this.method_2(this.roomModel_0));
            base.WriteC((byte) this.roomModel_0.MapId);
            base.WriteC((byte) this.roomModel_0.Rule);
            base.WriteC((byte) this.roomModel_0.Stage);
            base.WriteC((byte) this.roomModel_0.RoomType);
        }
    }
}

