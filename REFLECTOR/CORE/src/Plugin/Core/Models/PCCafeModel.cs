namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PCCafeModel
    {
        public PCCafeModel(CafeEnum cafeEnum_1)
        {
            this.Type = cafeEnum_1;
            this.PointUp = 0;
            this.ExpUp = 0;
        }

        public CafeEnum Type { get; set; }

        public int PointUp { get; set; }

        public int ExpUp { get; set; }

        public SortedList<CafeEnum, List<ItemsModel>> Rewards { get; set; }
    }
}

