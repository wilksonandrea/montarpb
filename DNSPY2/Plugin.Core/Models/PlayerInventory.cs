using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Plugin.Core.Models
{
	// Token: 0x02000090 RID: 144
	public class PlayerInventory
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00005D6C File Offset: 0x00003F6C
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00005D74 File Offset: 0x00003F74
		public List<ItemsModel> Items
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00005D7D File Offset: 0x00003F7D
		public PlayerInventory()
		{
			this.Items = new List<ItemsModel>();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public ItemsModel GetItem(int Id)
		{
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				foreach (ItemsModel itemsModel in this.Items)
				{
					if (itemsModel.Id == Id)
					{
						return itemsModel;
					}
				}
			}
			return null;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001C384 File Offset: 0x0001A584
		public ItemsModel GetItem(long ObjectId)
		{
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				foreach (ItemsModel itemsModel in this.Items)
				{
					if (itemsModel.ObjectId == ObjectId)
					{
						return itemsModel;
					}
				}
			}
			return null;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001C40C File Offset: 0x0001A60C
		public List<ItemsModel> GetItemsByType(ItemCategory Type)
		{
			List<ItemsModel> list = new List<ItemsModel>();
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				foreach (ItemsModel itemsModel in this.Items)
				{
					if (itemsModel.Category == Type || (itemsModel.Id > 1600000 && itemsModel.Id < 1700000 && Type == ItemCategory.NewItem))
					{
						list.Add(itemsModel);
					}
				}
			}
			return list;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public bool RemoveItem(ItemsModel Item)
		{
			List<ItemsModel> items = this.Items;
			bool flag2;
			lock (items)
			{
				flag2 = this.Items.Remove(Item);
			}
			return flag2;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001C504 File Offset: 0x0001A704
		public void AddItem(ItemsModel Item)
		{
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				this.Items.Add(Item);
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C54C File Offset: 0x0001A74C
		public void LoadBasicItems()
		{
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				this.Items.AddRange(TemplatePackXML.Basics);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001C598 File Offset: 0x0001A798
		public void LoadGeneralBeret()
		{
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				this.Items.Add(new ItemsModel(2700008, "Beret S. General", ItemEquipType.Permanent, 1U));
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		public void LoadHatForGM()
		{
			List<ItemsModel> items = this.Items;
			lock (items)
			{
				this.Items.Add(new ItemsModel(700160, "MOD Hat", ItemEquipType.Permanent, 1U));
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001C648 File Offset: 0x0001A848
		public byte[] EquipmentData(int ItemId)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				ItemsModel item = this.GetItem(ItemId);
				if (item != null)
				{
					syncServerPacket.WriteD(item.Id);
					syncServerPacket.WriteD((uint)item.ObjectId);
				}
				else
				{
					syncServerPacket.WriteD(ItemId);
					syncServerPacket.WriteD(0);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040002D0 RID: 720
		[CompilerGenerated]
		private List<ItemsModel> list_0;
	}
}
