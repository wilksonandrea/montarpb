using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Server.Match.Data.Models
{
	// Token: 0x02000037 RID: 55
	public class DeathServerData
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000025D0 File Offset: 0x000007D0
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000025D8 File Offset: 0x000007D8
		public CharaDeath DeathType
		{
			[CompilerGenerated]
			get
			{
				return this.charaDeath_0;
			}
			[CompilerGenerated]
			set
			{
				this.charaDeath_0 = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000025E1 File Offset: 0x000007E1
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000025E9 File Offset: 0x000007E9
		public PlayerModel Player
		{
			[CompilerGenerated]
			get
			{
				return this.playerModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.playerModel_0 = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000025F2 File Offset: 0x000007F2
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000025FA File Offset: 0x000007FA
		public int AssistSlot
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

		// Token: 0x06000117 RID: 279 RVA: 0x00002603 File Offset: 0x00000803
		public DeathServerData()
		{
			this.AssistSlot = -1;
		}

		// Token: 0x04000035 RID: 53
		[CompilerGenerated]
		private CharaDeath charaDeath_0;

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		private PlayerModel playerModel_0;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		private int int_0;
	}
}
