namespace Server.Match.Data.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class AssistServerData
    {
        public AssistServerData()
        {
            this.Killer = -1;
            this.Victim = -1;
        }

        public int RoomId { get; set; }

        public int Killer { get; set; }

        public int Victim { get; set; }

        public int Damage { get; set; }

        public bool IsAssist { get; set; }

        public bool IsKiller { get; set; }

        public bool VictimDead { get; set; }
    }
}

