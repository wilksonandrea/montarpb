namespace Plugin.Core.Enums
{
    using System;

    [Flags]
    public enum ResultIcon
    {
        None = 0,
        Pc = 1,
        PcPlus = 2,
        Item = 4,
        Event = 8,
        Unk = 0x10
    }
}

