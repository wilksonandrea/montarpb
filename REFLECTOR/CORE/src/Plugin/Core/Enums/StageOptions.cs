namespace Plugin.Core.Enums
{
    using System;

    public enum StageOptions : byte
    {
        None = 0,
        Default = 1,
        AI = 2,
        DieHard = 4,
        Infection = 6,
        Sniper = 0x60,
        Shotgun = 0x80,
        Knuckle = 0xe0
    }
}

