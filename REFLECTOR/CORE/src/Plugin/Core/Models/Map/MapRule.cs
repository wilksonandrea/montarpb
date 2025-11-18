namespace Plugin.Core.Models.Map
{
    using System;
    using System.Runtime.CompilerServices;

    public class MapRule
    {
        public MapRule()
        {
            this.Name = "";
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public int Rule { get; set; }

        public int StageOptions { get; set; }

        public int Conditions { get; set; }
    }
}

