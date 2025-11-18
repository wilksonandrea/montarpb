namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Runtime.CompilerServices;

    public class EventBoostModel
    {
        public EventBoostModel()
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

        public int BonusExp { get; set; }

        public int BonusGold { get; set; }

        public int Percent { get; set; }

        public uint BeginDate { get; set; }

        public uint EndedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Period { get; set; }

        public bool Priority { get; set; }

        public PortalBoostEvent BoostType { get; set; }

        public int BoostValue { get; set; }
    }
}

