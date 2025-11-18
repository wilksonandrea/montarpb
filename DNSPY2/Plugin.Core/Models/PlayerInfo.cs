using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x0200008F RID: 143
	public class PlayerInfo
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00005C6F File Offset: 0x00003E6F
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00005C77 File Offset: 0x00003E77
		public int Rank
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

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00005C80 File Offset: 0x00003E80
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00005C88 File Offset: 0x00003E88
		public int NickColor
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

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00005C91 File Offset: 0x00003E91
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00005C99 File Offset: 0x00003E99
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00005CA2 File Offset: 0x00003EA2
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00005CAA File Offset: 0x00003EAA
		public string Nickname
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00005CB3 File Offset: 0x00003EB3
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00005CBB File Offset: 0x00003EBB
		public bool IsOnline
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

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00005CC4 File Offset: 0x00003EC4
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00005CCC File Offset: 0x00003ECC
		public AccountStatus Status
		{
			[CompilerGenerated]
			get
			{
				return this.accountStatus_0;
			}
			[CompilerGenerated]
			set
			{
				this.accountStatus_0 = value;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00005CD5 File Offset: 0x00003ED5
		public PlayerInfo(long long_1)
		{
			this.PlayerId = long_1;
			this.Status = new AccountStatus();
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00005CEF File Offset: 0x00003EEF
		public PlayerInfo(long long_1, int int_2, int int_3, string string_1, bool bool_1, AccountStatus accountStatus_1)
		{
			this.PlayerId = long_1;
			this.SetInfo(int_2, int_3, string_1, bool_1, accountStatus_1);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00005D0C File Offset: 0x00003F0C
		public void SetOnlineStatus(bool state)
		{
			if (this.IsOnline != state && ComDiv.UpdateDB("accounts", "online", state, "player_id", this.PlayerId))
			{
				this.IsOnline = state;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00005D45 File Offset: 0x00003F45
		public void SetInfo(int Rank, int NickColor, string Nickname, bool IsOnline, AccountStatus Status)
		{
			this.Rank = Rank;
			this.NickColor = NickColor;
			this.Nickname = Nickname;
			this.IsOnline = IsOnline;
			this.Status = Status;
		}

		// Token: 0x040002CA RID: 714
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002CB RID: 715
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002CC RID: 716
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040002CD RID: 717
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040002CE RID: 718
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x040002CF RID: 719
		[CompilerGenerated]
		private AccountStatus accountStatus_0;
	}
}
