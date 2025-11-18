namespace Server.Match.Data.Enums
{
    using System;

    [Flags]
    public enum BombFlag
    {
        Start = 1,
        Stop = 2,
        Defuse = 4,
        Unknown = 8
    }
}

