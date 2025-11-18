namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class GoodsItem
    {
        public GoodsItem()
        {
            ItemsModel model1 = new ItemsModel();
            model1.Equip = ItemEquipType.Durable;
            ItemsModel model = model1;
            this.Item = model;
        }

        public int Id { get; set; }

        public int PriceGold { get; set; }

        public int PriceCash { get; set; }

        public int AuthType { get; set; }

        public int BuyType2 { get; set; }

        public int BuyType3 { get; set; }

        public int Title { get; set; }

        public int Visibility { get; set; }

        public int StarGold { get; set; }

        public int StarCash { get; set; }

        public ItemTag Tag { get; set; }

        public ItemsModel Item { get; set; }
    }
}

