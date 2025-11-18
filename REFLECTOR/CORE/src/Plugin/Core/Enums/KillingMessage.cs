namespace Plugin.Core.Enums
{
    using System;

    [Flags]
    public enum KillingMessage
    {
        None = 0,
        PiercingShot = 1,
        MassKill = 2,
        ChainStopper = 4,
        Headshot = 8,
        ChainHeadshot = 0x10,
        ChainSlugger = 0x20,
        Suicide = 0x40,
        ObjectDefense = 0x80
    }
}

