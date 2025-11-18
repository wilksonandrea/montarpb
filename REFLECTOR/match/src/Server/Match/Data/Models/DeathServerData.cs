namespace Server.Match.Data.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class DeathServerData
    {
        public DeathServerData()
        {
            this.AssistSlot = -1;
        }

        public CharaDeath DeathType { get; set; }

        public PlayerModel Player { get; set; }

        public int AssistSlot { get; set; }
    }
}

