using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class GoodsItem
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private int int_7;

	[CompilerGenerated]
	private int int_8;

	[CompilerGenerated]
	private int int_9;

	[CompilerGenerated]
	private ItemTag itemTag_0;

	[CompilerGenerated]
	private ItemsModel itemsModel_0;

	public int Id
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public int PriceGold
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public int PriceCash
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public int AuthType
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public int BuyType2
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public int BuyType3
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	public int Title
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	public int Visibility
	{
		[CompilerGenerated]
		get
		{
			return int_7;
		}
		[CompilerGenerated]
		set
		{
			int_7 = value;
		}
	}

	public int StarGold
	{
		[CompilerGenerated]
		get
		{
			return int_8;
		}
		[CompilerGenerated]
		set
		{
			int_8 = value;
		}
	}

	public int StarCash
	{
		[CompilerGenerated]
		get
		{
			return int_9;
		}
		[CompilerGenerated]
		set
		{
			int_9 = value;
		}
	}

	public ItemTag Tag
	{
		[CompilerGenerated]
		get
		{
			return itemTag_0;
		}
		[CompilerGenerated]
		set
		{
			itemTag_0 = value;
		}
	}

	public ItemsModel Item
	{
		[CompilerGenerated]
		get
		{
			return itemsModel_0;
		}
		[CompilerGenerated]
		set
		{
			itemsModel_0 = value;
		}
	}

	public GoodsItem()
	{
		ItemsModel ıtem = new ItemsModel
		{
			Equip = ItemEquipType.Durable
		};
		Item = ıtem;
	}
}
