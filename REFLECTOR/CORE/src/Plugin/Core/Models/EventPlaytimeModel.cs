namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EventPlaytimeModel
    {
        public EventPlaytimeModel()
        {
            this.Name = "";
            this.Description = "";
        }

        public bool EventIsEnabled()
        {
            uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            return ((this.BeginDate <= num) && (num < this.EndedDate));
        }

        public int Id { get; set; }

        public uint BeginDate { get; set; }

        public uint EndedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Minutes1 { get; set; }

        public int Minutes2 { get; set; }

        public int Minutes3 { get; set; }

        public bool Period { get; set; }

        public bool Priority { get; set; }

        public List<int> Goods1 { get; set; }

        public List<int> Goods2 { get; set; }

        public List<int> Goods3 { get; set; }
    }
}

