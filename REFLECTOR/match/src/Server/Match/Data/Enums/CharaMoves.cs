namespace Server.Match.Data.Enums
{
    using System;

    [Flags]
    public enum CharaMoves
    {
        Stop = 0,
        Left = 1,
        Back = 2,
        Right = 4,
        Front = 8,
        HeliInMove = 0x10,
        HeliUnknown = 0x20,
        HeliLeave = 0x40,
        HeliStopped = 0x80
    }
}

