using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200009A RID: 154
	public class FragModel
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00006101 File Offset: 0x00004301
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00006109 File Offset: 0x00004309
		public byte WeaponClass
		{
			[CompilerGenerated]
			get
			{
				return this.byte_0;
			}
			[CompilerGenerated]
			set
			{
				this.byte_0 = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00006112 File Offset: 0x00004312
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0000611A File Offset: 0x0000431A
		public byte HitspotInfo
		{
			[CompilerGenerated]
			get
			{
				return this.byte_1;
			}
			[CompilerGenerated]
			set
			{
				this.byte_1 = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00006123 File Offset: 0x00004323
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0000612B File Offset: 0x0000432B
		public byte Unk
		{
			[CompilerGenerated]
			get
			{
				return this.byte_2;
			}
			[CompilerGenerated]
			set
			{
				this.byte_2 = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00006134 File Offset: 0x00004334
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0000613C File Offset: 0x0000433C
		public KillingMessage KillFlag
		{
			[CompilerGenerated]
			get
			{
				return this.killingMessage_0;
			}
			[CompilerGenerated]
			set
			{
				this.killingMessage_0 = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00006145 File Offset: 0x00004345
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0000614D File Offset: 0x0000434D
		public float X
		{
			[CompilerGenerated]
			get
			{
				return this.float_0;
			}
			[CompilerGenerated]
			set
			{
				this.float_0 = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00006156 File Offset: 0x00004356
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x0000615E File Offset: 0x0000435E
		public float Y
		{
			[CompilerGenerated]
			get
			{
				return this.float_1;
			}
			[CompilerGenerated]
			set
			{
				this.float_1 = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00006167 File Offset: 0x00004367
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0000616F File Offset: 0x0000436F
		public float Z
		{
			[CompilerGenerated]
			get
			{
				return this.float_2;
			}
			[CompilerGenerated]
			set
			{
				this.float_2 = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00006178 File Offset: 0x00004378
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00006180 File Offset: 0x00004380
		public byte VictimSlot
		{
			[CompilerGenerated]
			get
			{
				return this.byte_3;
			}
			[CompilerGenerated]
			set
			{
				this.byte_3 = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00006189 File Offset: 0x00004389
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00006191 File Offset: 0x00004391
		public byte AssistSlot
		{
			[CompilerGenerated]
			get
			{
				return this.byte_4;
			}
			[CompilerGenerated]
			set
			{
				this.byte_4 = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0000619A File Offset: 0x0000439A
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x000061A2 File Offset: 0x000043A2
		public byte[] Unks
		{
			[CompilerGenerated]
			get
			{
				return this.byte_5;
			}
			[CompilerGenerated]
			set
			{
				this.byte_5 = value;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00002116 File Offset: 0x00000316
		public FragModel()
		{
		}

		// Token: 0x040002FF RID: 767
		[CompilerGenerated]
		private byte byte_0;

		// Token: 0x04000300 RID: 768
		[CompilerGenerated]
		private byte byte_1;

		// Token: 0x04000301 RID: 769
		[CompilerGenerated]
		private byte byte_2;

		// Token: 0x04000302 RID: 770
		[CompilerGenerated]
		private KillingMessage killingMessage_0;

		// Token: 0x04000303 RID: 771
		[CompilerGenerated]
		private float float_0;

		// Token: 0x04000304 RID: 772
		[CompilerGenerated]
		private float float_1;

		// Token: 0x04000305 RID: 773
		[CompilerGenerated]
		private float float_2;

		// Token: 0x04000306 RID: 774
		[CompilerGenerated]
		private byte byte_3;

		// Token: 0x04000307 RID: 775
		[CompilerGenerated]
		private byte byte_4;

		// Token: 0x04000308 RID: 776
		[CompilerGenerated]
		private byte[] byte_5;
	}
}
