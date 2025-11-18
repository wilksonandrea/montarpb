using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200008C RID: 140
	public class PlayerBonus
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00005963 File Offset: 0x00003B63
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000596B File Offset: 0x00003B6B
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

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00005974 File Offset: 0x00003B74
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000597C File Offset: 0x00003B7C
		public int Bonuses
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00005985 File Offset: 0x00003B85
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0000598D File Offset: 0x00003B8D
		public int CrosshairColor
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

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00005996 File Offset: 0x00003B96
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0000599E File Offset: 0x00003B9E
		public int MuzzleColor
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

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000059A7 File Offset: 0x00003BA7
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x000059AF File Offset: 0x00003BAF
		public int FreePass
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000059B8 File Offset: 0x00003BB8
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x000059C0 File Offset: 0x00003BC0
		public int FakeRank
		{
			[CompilerGenerated]
			get
			{
				return this.int_4;
			}
			[CompilerGenerated]
			set
			{
				this.int_4 = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x000059C9 File Offset: 0x00003BC9
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x000059D1 File Offset: 0x00003BD1
		public int NickBorderColor
		{
			[CompilerGenerated]
			get
			{
				return this.int_5;
			}
			[CompilerGenerated]
			set
			{
				this.int_5 = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x000059DA File Offset: 0x00003BDA
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x000059E2 File Offset: 0x00003BE2
		public string FakeNick
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

		// Token: 0x0600066F RID: 1647 RVA: 0x000059EB File Offset: 0x00003BEB
		public PlayerBonus()
		{
			this.CrosshairColor = 4;
			this.FakeRank = 55;
			this.FakeNick = "";
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001C004 File Offset: 0x0001A204
		public bool RemoveBonuses(int ItemId)
		{
			int bonuses = this.Bonuses;
			int freePass = this.FreePass;
			if (ItemId <= 1600037)
			{
				switch (ItemId)
				{
				case 1600001:
					this.method_0(1);
					break;
				case 1600002:
					this.method_0(2);
					break;
				case 1600003:
					this.method_0(4);
					break;
				case 1600004:
					this.method_0(16);
					break;
				default:
					if (ItemId != 1600011)
					{
						if (ItemId == 1600037)
						{
							this.method_0(8);
						}
					}
					else
					{
						this.method_2(128);
					}
					break;
				}
			}
			else if (ItemId != 1600038)
			{
				if (ItemId != 1600119)
				{
					switch (ItemId)
					{
					case 1600201:
						this.method_0(512);
						break;
					case 1600202:
						this.method_0(1024);
						break;
					case 1600203:
						this.method_0(2048);
						break;
					case 1600204:
						this.method_0(4096);
						break;
					}
				}
				else
				{
					this.method_0(32);
				}
			}
			else
			{
				this.method_0(64);
			}
			return this.Bonuses != bonuses || this.FreePass != freePass;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001C12C File Offset: 0x0001A32C
		public bool AddBonuses(int ItemId)
		{
			int bonuses = this.Bonuses;
			int freePass = this.FreePass;
			if (ItemId <= 1600037)
			{
				switch (ItemId)
				{
				case 1600001:
					this.method_1(1);
					break;
				case 1600002:
					this.method_1(2);
					break;
				case 1600003:
					this.method_1(4);
					break;
				case 1600004:
					this.method_1(16);
					break;
				default:
					if (ItemId != 1600011)
					{
						if (ItemId == 1600037)
						{
							this.method_1(8);
						}
					}
					else
					{
						this.method_3(128);
					}
					break;
				}
			}
			else if (ItemId != 1600038)
			{
				if (ItemId != 1600119)
				{
					switch (ItemId)
					{
					case 1600201:
						this.method_1(512);
						break;
					case 1600202:
						this.method_1(1024);
						break;
					case 1600203:
						this.method_1(2048);
						break;
					case 1600204:
						this.method_1(4096);
						break;
					}
				}
				else
				{
					this.method_1(32);
				}
			}
			else
			{
				this.method_1(64);
			}
			return this.Bonuses != bonuses || this.FreePass != freePass;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00005A0D File Offset: 0x00003C0D
		private void method_0(int int_6)
		{
			this.Bonuses &= ~int_6;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00005A1E File Offset: 0x00003C1E
		private void method_1(int int_6)
		{
			this.Bonuses |= int_6;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00005A2E File Offset: 0x00003C2E
		private void method_2(int int_6)
		{
			this.FreePass &= ~int_6;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00005A3F File Offset: 0x00003C3F
		private void method_3(int int_6)
		{
			this.FreePass |= int_6;
		}

		// Token: 0x040002A2 RID: 674
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040002A3 RID: 675
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002A4 RID: 676
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002A5 RID: 677
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040002A6 RID: 678
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040002A7 RID: 679
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040002A8 RID: 680
		[CompilerGenerated]
		private int int_5;

		// Token: 0x040002A9 RID: 681
		[CompilerGenerated]
		private string string_0;
	}
}
