using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Plugin.Core.Models;

public class PlayerInventory
{
	[CompilerGenerated]
	private List<ItemsModel> list_0;

	public List<ItemsModel> Items
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public PlayerInventory()
	{
		Items = new List<ItemsModel>();
	}

	public ItemsModel GetItem(int Id)
	{
		lock (Items)
		{
			foreach (ItemsModel ıtem in Items)
			{
				if (ıtem.Id == Id)
				{
					return ıtem;
				}
			}
		}
		return null;
	}

	public ItemsModel GetItem(long ObjectId)
	{
		lock (Items)
		{
			foreach (ItemsModel ıtem in Items)
			{
				if (ıtem.ObjectId == ObjectId)
				{
					return ıtem;
				}
			}
		}
		return null;
	}

	public List<ItemsModel> GetItemsByType(ItemCategory Type)
	{
		List<ItemsModel> list = new List<ItemsModel>();
		lock (Items)
		{
			foreach (ItemsModel ıtem in Items)
			{
				if (ıtem.Category == Type || (ıtem.Id > 1600000 && ıtem.Id < 1700000 && Type == ItemCategory.NewItem))
				{
					list.Add(ıtem);
				}
			}
			return list;
		}
	}

	public bool RemoveItem(ItemsModel Item)
	{
		lock (Items)
		{
			return Items.Remove(Item);
		}
	}

	public void AddItem(ItemsModel Item)
	{
		lock (Items)
		{
			Items.Add(Item);
		}
	}

	public void LoadBasicItems()
	{
		lock (Items)
		{
			Items.AddRange(TemplatePackXML.Basics);
		}
	}

	public void LoadGeneralBeret()
	{
		lock (Items)
		{
			Items.Add(new ItemsModel(2700008, "Beret S. General", ItemEquipType.Permanent, 1u));
		}
	}

	public void LoadHatForGM()
	{
		lock (Items)
		{
			Items.Add(new ItemsModel(700160, "MOD Hat", ItemEquipType.Permanent, 1u));
		}
	}

	public byte[] EquipmentData(int ItemId)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		ItemsModel ıtem = GetItem(ItemId);
		if (ıtem != null)
		{
			syncServerPacket.WriteD(ıtem.Id);
			syncServerPacket.WriteD((uint)ıtem.ObjectId);
		}
		else
		{
			syncServerPacket.WriteD(ItemId);
			syncServerPacket.WriteD(0);
		}
		return syncServerPacket.ToArray();
	}
}
