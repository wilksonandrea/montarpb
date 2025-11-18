using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x0200008B RID: 139
	public class ItemsModel
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000058B4 File Offset: 0x00003AB4
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x000058BC File Offset: 0x00003ABC
		public int Id
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x000058C5 File Offset: 0x00003AC5
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x000058CD File Offset: 0x00003ACD
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000058D6 File Offset: 0x00003AD6
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x000058DE File Offset: 0x00003ADE
		public ItemCategory Category
		{
			[CompilerGenerated]
			get
			{
				return this.itemCategory_0;
			}
			[CompilerGenerated]
			set
			{
				this.itemCategory_0 = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000058E7 File Offset: 0x00003AE7
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x000058EF File Offset: 0x00003AEF
		public ItemEquipType Equip
		{
			[CompilerGenerated]
			get
			{
				return this.itemEquipType_0;
			}
			[CompilerGenerated]
			set
			{
				this.itemEquipType_0 = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000058F8 File Offset: 0x00003AF8
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00005900 File Offset: 0x00003B00
		public long ObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.long_0;
			}
			[CompilerGenerated]
			set
			{
				this.long_0 = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00005909 File Offset: 0x00003B09
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00005911 File Offset: 0x00003B11
		public uint Count
		{
			[CompilerGenerated]
			get
			{
				return this.uint_0;
			}
			[CompilerGenerated]
			set
			{
				this.uint_0 = value;
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00002116 File Offset: 0x00000316
		public ItemsModel()
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000591A File Offset: 0x00003B1A
		public ItemsModel(int int_1)
		{
			this.SetItemId(int_1);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00005929 File Offset: 0x00003B29
		public ItemsModel(int int_1, string string_1, ItemEquipType itemEquipType_1, uint uint_1)
		{
			this.SetItemId(int_1);
			this.Name = string_1;
			this.Equip = itemEquipType_1;
			this.Count = uint_1;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public ItemsModel(ItemsModel itemsModel_0)
		{
			this.Id = itemsModel_0.Id;
			this.Name = itemsModel_0.Name;
			this.Count = itemsModel_0.Count;
			this.Equip = itemsModel_0.Equip;
			this.Category = itemsModel_0.Category;
			this.ObjectId = itemsModel_0.ObjectId;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000594E File Offset: 0x00003B4E
		public void SetItemId(int Id)
		{
			this.Id = Id;
			this.Category = ComDiv.GetItemCategory(Id);
		}

		// Token: 0x0400029C RID: 668
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400029D RID: 669
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400029E RID: 670
		[CompilerGenerated]
		private ItemCategory itemCategory_0;

		// Token: 0x0400029F RID: 671
		[CompilerGenerated]
		private ItemEquipType itemEquipType_0;

		// Token: 0x040002A0 RID: 672
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040002A1 RID: 673
		[CompilerGenerated]
		private uint uint_0;
	}
}
