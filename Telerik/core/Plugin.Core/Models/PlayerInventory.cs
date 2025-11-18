using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerInventory
	{
		public List<ItemsModel> Items
		{
			get;
			set;
		}

		public PlayerInventory()
		{
			this.Items = new List<ItemsModel>();
		}

		public void AddItem(ItemsModel Item)
		{
			lock (this.Items)
			{
				this.Items.Add(Item);
			}
		}

		public byte[] EquipmentData(int ItemId)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				ItemsModel ıtem = this.GetItem(ItemId);
				if (ıtem == null)
				{
					syncServerPacket.WriteD(ItemId);
					syncServerPacket.WriteD(0);
				}
				else
				{
					syncServerPacket.WriteD(ıtem.Id);
					syncServerPacket.WriteD((uint)ıtem.ObjectId);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public ItemsModel GetItem(int Id)
		{
			ItemsModel ıtemsModel;
			lock (this.Items)
			{
				List<ItemsModel>.Enumerator enumerator = this.Items.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ItemsModel current = enumerator.Current;
						if (current.Id != Id)
						{
							continue;
						}
						ıtemsModel = current;
						return ıtemsModel;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return ıtemsModel;
		}

		public ItemsModel GetItem(long ObjectId)
		{
			ItemsModel ıtemsModel;
			lock (this.Items)
			{
				List<ItemsModel>.Enumerator enumerator = this.Items.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ItemsModel current = enumerator.Current;
						if (current.ObjectId != ObjectId)
						{
							continue;
						}
						ıtemsModel = current;
						return ıtemsModel;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return ıtemsModel;
		}

		public List<ItemsModel> GetItemsByType(ItemCategory Type)
		{
			List<ItemsModel> ıtemsModels = new List<ItemsModel>();
			lock (this.Items)
			{
				foreach (ItemsModel ıtem in this.Items)
				{
					if (ıtem.Category != Type && (ıtem.Id <= 1600000 || ıtem.Id >= 1700000 || Type != ItemCategory.NewItem))
					{
						continue;
					}
					ıtemsModels.Add(ıtem);
				}
			}
			return ıtemsModels;
		}

		public void LoadBasicItems()
		{
			lock (this.Items)
			{
				this.Items.AddRange(TemplatePackXML.Basics);
			}
		}

		public void LoadGeneralBeret()
		{
			lock (this.Items)
			{
				this.Items.Add(new ItemsModel(2700008, "Beret S. General", ItemEquipType.Permanent, 1));
			}
		}

		public void LoadHatForGM()
		{
			lock (this.Items)
			{
				this.Items.Add(new ItemsModel(700160, "MOD Hat", ItemEquipType.Permanent, 1));
			}
		}

		public bool RemoveItem(ItemsModel Item)
		{
			bool flag;
			lock (this.Items)
			{
				flag = this.Items.Remove(Item);
			}
			return flag;
		}
	}
}