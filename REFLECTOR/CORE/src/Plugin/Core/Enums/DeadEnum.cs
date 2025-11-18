namespace Plugin.Core.Enums
{
    using System;

    [Flags]
    public enum DeadEnum
    {
        Alive = 1,
        Dead = 2,
        UseChat = 4
    }
}

