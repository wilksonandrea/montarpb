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

    public class PROTOCOL_BATTLE_RESPAWN_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly SlotModel slotModel_0;
        private readonly PlayerEquipment playerEquipment_0;
        private readonly List<int> list_0;
        private readonly int int_0;

        public PROTOCOL_BATTLE_RESPAWN_ACK(RoomModel roomModel_1, SlotModel slotModel_1)
        {
            this.roomModel_0 = roomModel_1;
            this.slotModel_0 = slotModel_1;
            if ((roomModel_1 != null) && (slotModel_1 != null))
            {
                this.playerEquipment_0 = slotModel_1.Equipment;
                if (this.playerEquipment_0 != null)
                {
                    TeamEnum enum2 = roomModel_1.ValidateTeam(slotModel_1.Team, slotModel_1.CostumeTeam);
                    if (enum2 == TeamEnum.FR_TEAM)
                    {
                        this.int_0 = this.playerEquipment_0.CharaRedId;
                    }
                    else if (enum2 == TeamEnum.CT_TEAM)
                    {
                        this.int_0 = this.playerEquipment_0.CharaBlueId;
                    }
                }
                this.list_0 = AllUtils.GetDinossaurs(roomModel_1, false, slotModel_1.Id);
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
            base.WriteH((short) 0x1412);
            base.WriteD(this.slotModel_0.Id);
            int spawnsCount = this.roomModel_0.SpawnsCount;
            this.roomModel_0.SpawnsCount = spawnsCount + 1;
            base.WriteD(spawnsCount);
            spawnsCount = this.slotModel_0.SpawnsCount + 1;
            this.slotModel_0.SpawnsCount = spawnsCount;
            base.WriteD(spawnsCount);
            base.WriteD(this.playerEquipment_0.WeaponPrimary);
            base.WriteD(this.playerEquipment_0.WeaponSecondary);
            base.WriteD(this.playerEquipment_0.WeaponMelee);
            base.WriteD(this.playerEquipment_0.WeaponExplosive);
            base.WriteD(this.playerEquipment_0.WeaponSpecial);
            base.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
            base.WriteD(this.int_0);
            base.WriteD(this.playerEquipment_0.PartHead);
            base.WriteD(this.playerEquipment_0.PartFace);
            base.WriteD(this.playerEquipment_0.PartJacket);
            base.WriteD(this.playerEquipment_0.PartPocket);
            base.WriteD(this.playerEquipment_0.PartGlove);
            base.WriteD(this.playerEquipment_0.PartBelt);
            base.WriteD(this.playerEquipment_0.PartHolster);
            base.WriteD(this.playerEquipment_0.PartSkin);
            base.WriteD(this.playerEquipment_0.BeretItem);
            base.WriteD(this.playerEquipment_0.AccessoryId);
            base.WriteB(this.method_0(this.roomModel_0, this.list_0));
        }
    }
}

