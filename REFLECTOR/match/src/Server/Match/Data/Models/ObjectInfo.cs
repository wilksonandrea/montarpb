namespace Server.Match.Data.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ObjectInfo
    {
        public ObjectInfo(int int_3)
        {
            this.Id = int_3;
            this.Life = 100;
        }

        public int Id { get; set; }

        public int Life { get; set; }

        public int DestroyState { get; set; }

        public AnimModel Animation { get; set; }

        public DateTime UseDate { get; set; }

        public ObjectModel Model { get; set; }
    }
}

