using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class ItemsModel
	{
		public ItemCategory Category
		{
			get;
			set;
		}

		public uint Count
		{
			get;
			set;
		}

		public ItemEquipType Equip
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public long ObjectId
		{
			get;
			set;
		}

		public ItemsModel()
		{
		}

		public ItemsModel(int int_1)
		{
			this.SetItemId(int_1);
		}

		public ItemsModel(int int_1, string string_1, ItemEquipType itemEquipType_1, uint uint_1)
		{
			this.SetItemId(int_1);
			this.Name = string_1;
			this.Equip = itemEquipType_1;
			this.Count = uint_1;
		}

		public ItemsModel(ItemsModel itemsModel_0)
		{
			this.Id = itemsModel_0.Id;
			this.Name = itemsModel_0.Name;
			this.Count = itemsModel_0.Count;
			this.Equip = itemsModel_0.Equip;
			this.Category = itemsModel_0.Category;
			this.ObjectId = itemsModel_0.ObjectId;
		}

		public void SetItemId(int Id)
		{
			this.Id = Id;
			this.Category = ComDiv.GetItemCategory(Id);
		}
	}
}