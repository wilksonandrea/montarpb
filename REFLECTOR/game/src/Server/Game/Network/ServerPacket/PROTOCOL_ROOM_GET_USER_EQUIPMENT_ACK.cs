namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly PlayerEquipment playerEquipment_0;
        private readonly int[] int_0;
        private readonly byte byte_0;

        public PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(uint uint_1, PlayerEquipment playerEquipment_1, int[] int_1, byte byte_1)
        {
            this.uint_0 = uint_1;
            this.playerEquipment_0 = playerEquipment_1;
            this.int_0 = int_1;
            this.byte_0 = byte_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe52);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD(this.int_0[1]);
                base.WriteC(10);
                base.WriteD(this.int_0[0]);
                base.WriteD(this.playerEquipment_0.PartHead);
                base.WriteD(this.playerEquipment_0.PartFace);
                base.WriteD(this.playerEquipment_0.PartJacket);
                base.WriteD(this.playerEquipment_0.PartPocket);
                base.WriteD(this.playerEquipment_0.PartGlove);
                base.WriteD(this.playerEquipment_0.PartBelt);
                base.WriteD(this.playerEquipment_0.PartHolster);
                base.WriteD(this.playerEquipment_0.PartSkin);
                base.WriteD(this.playerEquipment_0.BeretItem);
                base.WriteC(5);
                base.WriteD(this.playerEquipment_0.WeaponPrimary);
                base.WriteD(this.playerEquipment_0.WeaponSecondary);
                base.WriteD(this.playerEquipment_0.WeaponMelee);
                base.WriteD(this.playerEquipment_0.WeaponExplosive);
                base.WriteD(this.playerEquipment_0.WeaponSpecial);
                base.WriteC(2);
                base.WriteD(this.playerEquipment_0.CharaRedId);
                base.WriteD(this.playerEquipment_0.CharaBlueId);
                base.WriteC(this.byte_0);
            }
        }
    }
}

