namespace Server.Match.Data.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class AnimModel
    {
        public int Id { get; set; }

        public int NextAnim { get; set; }

        public int OtherObj { get; set; }

        public int OtherAnim { get; set; }

        public float Duration { get; set; }
    }
}

