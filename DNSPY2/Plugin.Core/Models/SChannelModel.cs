using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200009E RID: 158
	public class SChannelModel
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00006361 File Offset: 0x00004561
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00006369 File Offset: 0x00004569
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00006372 File Offset: 0x00004572
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0000637A File Offset: 0x0000457A
		public int LastPlayers
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00006383 File Offset: 0x00004583
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0000638B File Offset: 0x0000458B
		public int MaxPlayers
		{
			[CompilerGenerated]
			get
			{
				return this.int_2;
			}
			[CompilerGenerated]
			set
			{
				this.int_2 = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00006394 File Offset: 0x00004594
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0000639C File Offset: 0x0000459C
		public int ChannelPlayers
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000063A5 File Offset: 0x000045A5
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x000063AD File Offset: 0x000045AD
		public bool State
		{
			[CompilerGenerated]
			get
			{
				return this.bool_0;
			}
			[CompilerGenerated]
			set
			{
				this.bool_0 = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000063B6 File Offset: 0x000045B6
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000063BE File Offset: 0x000045BE
		public SChannelType Type
		{
			[CompilerGenerated]
			get
			{
				return this.schannelType_0;
			}
			[CompilerGenerated]
			set
			{
				this.schannelType_0 = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000063C7 File Offset: 0x000045C7
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x000063CF File Offset: 0x000045CF
		public string Host
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000063D8 File Offset: 0x000045D8
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x000063E0 File Offset: 0x000045E0
		public ushort Port
		{
			[CompilerGenerated]
			get
			{
				return this.ushort_0;
			}
			[CompilerGenerated]
			set
			{
				this.ushort_0 = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000063E9 File Offset: 0x000045E9
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x000063F1 File Offset: 0x000045F1
		public bool IsMobile
		{
			[CompilerGenerated]
			get
			{
				return this.bool_1;
			}
			[CompilerGenerated]
			set
			{
				this.bool_1 = value;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000063FA File Offset: 0x000045FA
		public SChannelModel(string string_1, ushort ushort_1)
		{
			this.Host = string_1;
			this.Port = ushort_1;
		}

		// Token: 0x0400035A RID: 858
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400035B RID: 859
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400035C RID: 860
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400035D RID: 861
		[CompilerGenerated]
		private int int_3;

		// Token: 0x0400035E RID: 862
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x0400035F RID: 863
		[CompilerGenerated]
		private SChannelType schannelType_0;

		// Token: 0x04000360 RID: 864
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000361 RID: 865
		[CompilerGenerated]
		private ushort ushort_0;

		// Token: 0x04000362 RID: 866
		[CompilerGenerated]
		private bool bool_1;
	}
}
