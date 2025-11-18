using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000066 RID: 102
	public class PlayerQuickstart
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000046AF File Offset: 0x000028AF
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x000046B7 File Offset: 0x000028B7
		public long OwnerId
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000046C0 File Offset: 0x000028C0
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000046C8 File Offset: 0x000028C8
		public List<QuickstartModel> Quickjoins
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

		// Token: 0x06000450 RID: 1104 RVA: 0x000046D1 File Offset: 0x000028D1
		public PlayerQuickstart()
		{
			this.Quickjoins = new List<QuickstartModel>();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001B280 File Offset: 0x00019480
		public QuickstartModel GetMapList(byte MapId)
		{
			List<QuickstartModel> quickjoins = this.Quickjoins;
			lock (quickjoins)
			{
				foreach (QuickstartModel quickstartModel in this.Quickjoins)
				{
					if (quickstartModel.MapId == (int)MapId)
					{
						return quickstartModel;
					}
				}
			}
			return null;
		}

		// Token: 0x040001A9 RID: 425
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001AA RID: 426
		[CompilerGenerated]
		private List<QuickstartModel> list_0;
	}
}
