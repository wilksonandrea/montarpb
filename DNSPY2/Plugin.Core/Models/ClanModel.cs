using System;
using Plugin.Core.Enums;
using Plugin.Core.SQL;

namespace Plugin.Core.Models
{
	// Token: 0x02000084 RID: 132
	public class ClanModel
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		public ClanModel()
		{
			this.MaxPlayers = 50;
			this.Logo = uint.MaxValue;
			this.Name = "";
			this.Info = "";
			this.News = "";
			this.Points = 1000f;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000054DD File Offset: 0x000036DD
		public int GetClanUnit()
		{
			return this.GetClanUnit(DaoManagerSQL.GetClanPlayers(this.Id));
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000054F0 File Offset: 0x000036F0
		public int GetClanUnit(int Count)
		{
			if (Count >= 250)
			{
				return 7;
			}
			if (Count >= 200)
			{
				return 6;
			}
			if (Count >= 150)
			{
				return 5;
			}
			if (Count >= 100)
			{
				return 4;
			}
			if (Count >= 50)
			{
				return 3;
			}
			if (Count >= 30)
			{
				return 2;
			}
			if (Count >= 10)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000260 RID: 608
		public int Id;

		// Token: 0x04000261 RID: 609
		public int Matches;

		// Token: 0x04000262 RID: 610
		public int MatchWins;

		// Token: 0x04000263 RID: 611
		public int MatchLoses;

		// Token: 0x04000264 RID: 612
		public int TotalKills;

		// Token: 0x04000265 RID: 613
		public int TotalHeadshots;

		// Token: 0x04000266 RID: 614
		public int TotalDeaths;

		// Token: 0x04000267 RID: 615
		public int TotalAssists;

		// Token: 0x04000268 RID: 616
		public int TotalEscapes;

		// Token: 0x04000269 RID: 617
		public int Authority;

		// Token: 0x0400026A RID: 618
		public int RankLimit;

		// Token: 0x0400026B RID: 619
		public int MinAgeLimit;

		// Token: 0x0400026C RID: 620
		public int MaxAgeLimit;

		// Token: 0x0400026D RID: 621
		public int Exp;

		// Token: 0x0400026E RID: 622
		public int Rank;

		// Token: 0x0400026F RID: 623
		public int NameColor;

		// Token: 0x04000270 RID: 624
		public int MaxPlayers;

		// Token: 0x04000271 RID: 625
		public int Effect;

		// Token: 0x04000272 RID: 626
		public string Name;

		// Token: 0x04000273 RID: 627
		public string Info;

		// Token: 0x04000274 RID: 628
		public string News;

		// Token: 0x04000275 RID: 629
		public long OwnerId;

		// Token: 0x04000276 RID: 630
		public uint Logo;

		// Token: 0x04000277 RID: 631
		public uint CreationDate;

		// Token: 0x04000278 RID: 632
		public float Points;

		// Token: 0x04000279 RID: 633
		public JoinClanType JoinType;

		// Token: 0x0400027A RID: 634
		public ClanBestPlayers BestPlayers = new ClanBestPlayers();
	}
}
