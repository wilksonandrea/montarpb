using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000089 RID: 137
	public class CharacterModel
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00005817 File Offset: 0x00003A17
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0000581F File Offset: 0x00003A1F
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00005828 File Offset: 0x00003A28
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00005830 File Offset: 0x00003A30
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00005839 File Offset: 0x00003A39
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00005841 File Offset: 0x00003A41
		public int Slot
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000584A File Offset: 0x00003A4A
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00005852 File Offset: 0x00003A52
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000585B File Offset: 0x00003A5B
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00005863 File Offset: 0x00003A63
		public uint CreateDate
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

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000586C File Offset: 0x00003A6C
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00005874 File Offset: 0x00003A74
		public uint PlayTime
		{
			[CompilerGenerated]
			get
			{
				return this.uint_1;
			}
			[CompilerGenerated]
			set
			{
				this.uint_1 = value;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000587D File Offset: 0x00003A7D
		public CharacterModel()
		{
			this.Name = "";
		}

		// Token: 0x04000294 RID: 660
		[CompilerGenerated]
		private long long_0;

		// Token: 0x04000295 RID: 661
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000296 RID: 662
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000297 RID: 663
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000298 RID: 664
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x04000299 RID: 665
		[CompilerGenerated]
		private uint uint_1;
	}
}
