using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class ItemsModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private ItemCategory itemCategory_0;

	[CompilerGenerated]
	private ItemEquipType itemEquipType_0;

	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private uint uint_0;

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

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public ItemCategory Category
	{
		[CompilerGenerated]
		get
		{
			return itemCategory_0;
		}
		[CompilerGenerated]
		set
		{
			itemCategory_0 = value;
		}
	}

	public ItemEquipType Equip
	{
		[CompilerGenerated]
		get
		{
			return itemEquipType_0;
		}
		[CompilerGenerated]
		set
		{
			itemEquipType_0 = value;
		}
	}

	public long ObjectId
	{
		[CompilerGenerated]
		get
		{
			return long_0;
		}
		[CompilerGenerated]
		set
		{
			long_0 = value;
		}
	}

	public uint Count
	{
		[CompilerGenerated]
		get
		{
			return uint_0;
		}
		[CompilerGenerated]
		set
		{
			uint_0 = value;
		}
	}

	public ItemsModel()
	{
	}

	public ItemsModel(int int_1)
	{
		SetItemId(int_1);
	}

	public ItemsModel(int int_1, string string_1, ItemEquipType itemEquipType_1, uint uint_1)
	{
		SetItemId(int_1);
		Name = string_1;
		Equip = itemEquipType_1;
		Count = uint_1;
	}

	public ItemsModel(ItemsModel itemsModel_0)
	{
		Id = itemsModel_0.Id;
		Name = itemsModel_0.Name;
		Count = itemsModel_0.Count;
		Equip = itemsModel_0.Equip;
		Category = itemsModel_0.Category;
		ObjectId = itemsModel_0.ObjectId;
	}

	public void SetItemId(int Id)
	{
		this.Id = Id;
		Category = ComDiv.GetItemCategory(Id);
	}
}
