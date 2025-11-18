using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200006A RID: 106
	public class RecordInfo
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00004848 File Offset: 0x00002A48
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00004850 File Offset: 0x00002A50
		public long PlayerId
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00004859 File Offset: 0x00002A59
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00004861 File Offset: 0x00002A61
		public int RecordValue
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

		// Token: 0x06000477 RID: 1143 RVA: 0x0000486A File Offset: 0x00002A6A
		public RecordInfo(string[] string_0)
		{
			this.PlayerId = this.GetPlayerId(string_0);
			this.RecordValue = this.GetPlayerValue(string_0);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001B308 File Offset: 0x00019508
		public long GetPlayerId(string[] Split)
		{
			long num;
			try
			{
				num = long.Parse(Split[0]);
			}
			catch
			{
				num = 0L;
			}
			return num;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001B340 File Offset: 0x00019540
		public int GetPlayerValue(string[] Split)
		{
			int num;
			try
			{
				num = int.Parse(Split[1]);
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001B370 File Offset: 0x00019570
		public string GetSplit()
		{
			return this.PlayerId.ToString() + "-" + this.RecordValue.ToString();
		}

		// Token: 0x040001B8 RID: 440
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001B9 RID: 441
		[CompilerGenerated]
		private int int_0;
	}
}
