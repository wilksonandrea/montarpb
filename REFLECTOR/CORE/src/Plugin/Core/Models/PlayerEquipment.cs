namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerEquipment
    {
        public PlayerEquipment()
        {
            this.WeaponPrimary = 0x1925c;
            this.WeaponSecondary = 0x31513;
            this.WeaponMelee = 0x497c9;
            this.WeaponExplosive = 0x635d9;
            this.WeaponSpecial = 0x7c061;
            this.CharaRedId = 0x92ba9;
            this.CharaBlueId = 0x92f92;
            this.PartHead = 0x3ba57860;
            this.PartFace = 0x3ba6ff00;
            this.PartJacket = 0x3ba885a0;
            this.PartPocket = 0x3baa0c40;
            this.PartGlove = 0x3bab92e0;
            this.PartBelt = 0x3bad1980;
            this.PartHolster = 0x3baea020;
            this.PartSkin = 0x3bb026c0;
            this.DinoItem = 0x16e55f;
        }

        public long OwnerId { get; set; }

        public int WeaponPrimary { get; set; }

        public int WeaponSecondary { get; set; }

        public int WeaponMelee { get; set; }

        public int WeaponExplosive { get; set; }

        public int WeaponSpecial { get; set; }

        public int CharaRedId { get; set; }

        public int CharaBlueId { get; set; }

        public int PartHead { get; set; }

        public int PartFace { get; set; }

        public int PartJacket { get; set; }

        public int PartPocket { get; set; }

        public int PartGlove { get; set; }

        public int PartBelt { get; set; }

        public int PartHolster { get; set; }

        public int PartSkin { get; set; }

        public int BeretItem { get; set; }

        public int DinoItem { get; set; }

        public int AccessoryId { get; set; }

        public int SprayId { get; set; }

        public int NameCardId { get; set; }
    }
}

