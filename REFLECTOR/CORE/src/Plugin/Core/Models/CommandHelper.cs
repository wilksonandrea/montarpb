namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class CommandHelper
    {
        public CommandHelper(string string_1)
        {
            this.Tag = string_1;
        }

        public string Tag { get; set; }

        public int AllWeapons { get; set; }

        public int AssaultRifle { get; set; }

        public int SubMachineGun { get; set; }

        public int SniperRifle { get; set; }

        public int ShotGun { get; set; }

        public int MachineGun { get; set; }

        public int Secondary { get; set; }

        public int Melee { get; set; }

        public int Knuckle { get; set; }

        public int RPG7 { get; set; }

        public int Minutes05 { get; set; }

        public int Minutes10 { get; set; }

        public int Minutes15 { get; set; }

        public int Minutes20 { get; set; }

        public int Minutes25 { get; set; }

        public int Minutes30 { get; set; }
    }
}

