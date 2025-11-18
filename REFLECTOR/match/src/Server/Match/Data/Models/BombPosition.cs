namespace Server.Match.Data.Models
{
    using Plugin.Core.SharpDX;
    using System;
    using System.Runtime.CompilerServices;

    public class BombPosition
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Half3 Position { get; set; }

        public bool EveryWhere { get; set; }
    }
}

