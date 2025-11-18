using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200009D RID: 157
	public class VoteKickModel
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000062C8 File Offset: 0x000044C8
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x000062D0 File Offset: 0x000044D0
		public int CreatorIdx
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000062D9 File Offset: 0x000044D9
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x000062E1 File Offset: 0x000044E1
		public int VictimIdx
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

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000062EA File Offset: 0x000044EA
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x000062F2 File Offset: 0x000044F2
		public int Motive
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000062FB File Offset: 0x000044FB
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x00006303 File Offset: 0x00004503
		public int Accept
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000630C File Offset: 0x0000450C
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x00006314 File Offset: 0x00004514
		public int Denie
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000631D File Offset: 0x0000451D
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00006325 File Offset: 0x00004525
		public int Allies
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000632E File Offset: 0x0000452E
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00006336 File Offset: 0x00004536
		public int Enemies
		{
			[CompilerGenerated]
			get
			{
				return this.int_6;
			}
			[CompilerGenerated]
			set
			{
				this.int_6 = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000633F File Offset: 0x0000453F
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00006347 File Offset: 0x00004547
		public List<int> Votes
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00006350 File Offset: 0x00004550
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00006358 File Offset: 0x00004558
		public bool[] TotalArray
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

		// Token: 0x0600077C RID: 1916 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public VoteKickModel(int int_7, int int_8)
		{
			this.Accept = 1;
			this.Denie = 1;
			this.CreatorIdx = int_7;
			this.VictimIdx = int_8;
			this.Votes = new List<int>();
			this.Votes.Add(int_7);
			this.Votes.Add(int_8);
			this.TotalArray = new bool[18];
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		public int GetInGamePlayers()
		{
			int num = 0;
			for (int i = 0; i < 18; i++)
			{
				if (this.TotalArray[i])
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04000351 RID: 849
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000352 RID: 850
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000353 RID: 851
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000354 RID: 852
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000355 RID: 853
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000356 RID: 854
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000357 RID: 855
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000358 RID: 856
		[CompilerGenerated]
		private List<int> list_0;

		// Token: 0x04000359 RID: 857
		[CompilerGenerated]
		private bool[] bool_0;
	}
}
