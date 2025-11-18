namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class CharacterModel
    {
        public CharacterModel()
        {
            this.Name = "";
        }

        public long ObjectId { get; set; }

        public int Id { get; set; }

        public int Slot { get; set; }

        public string Name { get; set; }

        public uint CreateDate { get; set; }

        public uint PlayTime { get; set; }
    }
}

