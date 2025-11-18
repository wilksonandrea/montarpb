using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200009B RID: 155
	public class FragInfos
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x000061AB File Offset: 0x000043AB
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x000061B3 File Offset: 0x000043B3
		public byte KillerSlot
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000061BC File Offset: 0x000043BC
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x000061C4 File Offset: 0x000043C4
		public byte KillsCount
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x000061CD File Offset: 0x000043CD
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x000061D5 File Offset: 0x000043D5
		public byte Flag
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

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x000061DE File Offset: 0x000043DE
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x000061E6 File Offset: 0x000043E6
		public byte Unk
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x000061EF File Offset: 0x000043EF
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x000061F7 File Offset: 0x000043F7
		public CharaKillType KillingType
		{
			[CompilerGenerated]
			get
			{
				return this.charaKillType_0;
			}
			[CompilerGenerated]
			set
			{
				this.charaKillType_0 = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00006200 File Offset: 0x00004400
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00006208 File Offset: 0x00004408
		public int WeaponId
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00006211 File Offset: 0x00004411
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x00006219 File Offset: 0x00004419
		public int Score
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00006222 File Offset: 0x00004422
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0000622A File Offset: 0x0000442A
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

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00006233 File Offset: 0x00004433
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0000623B File Offset: 0x0000443B
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

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00006244 File Offset: 0x00004444
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0000624C File Offset: 0x0000444C
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00006255 File Offset: 0x00004455
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0000625D File Offset: 0x0000445D
		public List<FragModel> Frags
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

		// Token: 0x06000762 RID: 1890 RVA: 0x00006266 File Offset: 0x00004466
		public FragInfos()
		{
			this.Frags = new List<FragModel>();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001CA10 File Offset: 0x0001AC10
		public KillingMessage GetAllKillFlags()
		{
			KillingMessage killingMessage = KillingMessage.None;
			foreach (FragModel fragModel in this.Frags)
			{
				if (!killingMessage.HasFlag(fragModel.KillFlag))
				{
					killingMessage |= fragModel.KillFlag;
				}
			}
			return killingMessage;
		}

		// Token: 0x04000309 RID: 777
		[CompilerGenerated]
		private byte byte_0;

		// Token: 0x0400030A RID: 778
		[CompilerGenerated]
		private byte byte_1;

		// Token: 0x0400030B RID: 779
		[CompilerGenerated]
		private byte byte_2;

		// Token: 0x0400030C RID: 780
		[CompilerGenerated]
		private byte byte_3;

		// Token: 0x0400030D RID: 781
		[CompilerGenerated]
		private CharaKillType charaKillType_0;

		// Token: 0x0400030E RID: 782
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400030F RID: 783
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000310 RID: 784
		[CompilerGenerated]
		private float float_0;

		// Token: 0x04000311 RID: 785
		[CompilerGenerated]
		private float float_1;

		// Token: 0x04000312 RID: 786
		[CompilerGenerated]
		private float float_2;

		// Token: 0x04000313 RID: 787
		[CompilerGenerated]
		private List<FragModel> list_0;
	}
}
