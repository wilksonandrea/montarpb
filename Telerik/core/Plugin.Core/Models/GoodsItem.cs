using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class GoodsItem
	{
		public int AuthType
		{
			get;
			set;
		}

		public int BuyType2
		{
			get;
			set;
		}

		public int BuyType3
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public ItemsModel Item
		{
			get;
			set;
		}

		public int PriceCash
		{
			get;
			set;
		}

		public int PriceGold
		{
			get;
			set;
		}

		public int StarCash
		{
			get;
			set;
		}

		public int StarGold
		{
			get;
			set;
		}

		public ItemTag Tag
		{
			get;
			set;
		}

		public int Title
		{
			get;
			set;
		}

		public int Visibility
		{
			get;
			set;
		}

		public GoodsItem()
		{
			this.Item = new ItemsModel()
			{
				Equip = ItemEquipType.Durable
			};
		}
	}
}