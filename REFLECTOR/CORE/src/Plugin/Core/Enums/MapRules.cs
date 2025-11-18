namespace Plugin.Core.Enums
{
    using System;

    public enum MapRules : byte
    {
        None = 0,
        Space = 8,
        HeadHunter = 0x20,
        Chaos = 80,
        Round = 0x80,
        HARDCORE = 0xc0,
        HHHO = 0xe0
    }
}

