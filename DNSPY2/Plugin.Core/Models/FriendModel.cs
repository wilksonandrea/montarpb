using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000087 RID: 135
	public class FriendModel
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000566E File Offset: 0x0000386E
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00005676 File Offset: 0x00003876
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000567F File Offset: 0x0000387F
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00005687 File Offset: 0x00003887
		public long OwnerId
		{
			[CompilerGenerated]
			get
			{
				return this.long_1;
			}
			[CompilerGenerated]
			set
			{
				this.long_1 = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00005690 File Offset: 0x00003890
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00005698 File Offset: 0x00003898
		public long PlayerId
		{
			[CompilerGenerated]
			get
			{
				return this.long_2;
			}
			[CompilerGenerated]
			set
			{
				this.long_2 = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000056A1 File Offset: 0x000038A1
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x000056A9 File Offset: 0x000038A9
		public int State
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000056B2 File Offset: 0x000038B2
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x000056BA File Offset: 0x000038BA
		public bool Removed
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000056C3 File Offset: 0x000038C3
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x000056CB File Offset: 0x000038CB
		public PlayerInfo Info
		{
			[CompilerGenerated]
			get
			{
				return this.playerInfo_0;
			}
			[CompilerGenerated]
			set
			{
				this.playerInfo_0 = value;
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000056D4 File Offset: 0x000038D4
		public FriendModel(long long_3)
		{
			this.PlayerId = long_3;
			this.Info = new PlayerInfo(long_3);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000056EF File Offset: 0x000038EF
		public FriendModel(long long_3, int int_1, int int_2, string string_0, bool bool_1, AccountStatus accountStatus_0)
		{
			this.PlayerId = long_3;
			this.SetModel(long_3, int_1, int_2, string_0, bool_1, accountStatus_0);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000570D File Offset: 0x0000390D
		public void SetModel(long PlayerId, int Rank, int NickColor, string Nickname, bool IsOnline, AccountStatus Status)
		{
			this.Info = new PlayerInfo(PlayerId, Rank, NickColor, Nickname, IsOnline, Status);
		}

		// Token: 0x04000284 RID: 644
		[CompilerGenerated]
		private long long_0;

		// Token: 0x04000285 RID: 645
		[CompilerGenerated]
		private long long_1;

		// Token: 0x04000286 RID: 646
		[CompilerGenerated]
		private long long_2;

		// Token: 0x04000287 RID: 647
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000288 RID: 648
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x04000289 RID: 649
		[CompilerGenerated]
		private PlayerInfo playerInfo_0;
	}
}
