namespace Server.Match.Data.Enums
{
    using System;

    public enum WeaponSyncType
    {
        Primary = 0,
        Secondary = 0x10,
        Melee = 0x20,
        Explosive = 0x30,
        Special = 0x40,
        Mission = 80,
        Dual = 0x80,
        Ext = 0x90
    }
}

