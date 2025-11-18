using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000085 RID: 133
	public class ClanBestPlayers
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000552D File Offset: 0x0000372D
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00005535 File Offset: 0x00003735
		public RecordInfo Exp
		{
			[CompilerGenerated]
			get
			{
				return this.recordInfo_0;
			}
			[CompilerGenerated]
			set
			{
				this.recordInfo_0 = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000553E File Offset: 0x0000373E
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00005546 File Offset: 0x00003746
		public RecordInfo Participation
		{
			[CompilerGenerated]
			get
			{
				return this.recordInfo_1;
			}
			[CompilerGenerated]
			set
			{
				this.recordInfo_1 = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000554F File Offset: 0x0000374F
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00005557 File Offset: 0x00003757
		public RecordInfo Wins
		{
			[CompilerGenerated]
			get
			{
				return this.recordInfo_2;
			}
			[CompilerGenerated]
			set
			{
				this.recordInfo_2 = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00005560 File Offset: 0x00003760
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00005568 File Offset: 0x00003768
		public RecordInfo Kills
		{
			[CompilerGenerated]
			get
			{
				return this.recordInfo_3;
			}
			[CompilerGenerated]
			set
			{
				this.recordInfo_3 = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00005571 File Offset: 0x00003771
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00005579 File Offset: 0x00003779
		public RecordInfo Headshots
		{
			[CompilerGenerated]
			get
			{
				return this.recordInfo_4;
			}
			[CompilerGenerated]
			set
			{
				this.recordInfo_4 = value;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001B950 File Offset: 0x00019B50
		public void SetPlayers(string Exp, string Participation, string Wins, string Kills, string Headshots)
		{
			this.Exp = new RecordInfo(Exp.Split(new char[] { '-' }));
			this.Participation = new RecordInfo(Participation.Split(new char[] { '-' }));
			this.Wins = new RecordInfo(Wins.Split(new char[] { '-' }));
			this.Kills = new RecordInfo(Kills.Split(new char[] { '-' }));
			this.Headshots = new RecordInfo(Headshots.Split(new char[] { '-' }));
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001B9EC File Offset: 0x00019BEC
		public void SetDefault()
		{
			string[] array = new string[] { "0", "0" };
			this.Exp = new RecordInfo(array);
			this.Participation = new RecordInfo(array);
			this.Wins = new RecordInfo(array);
			this.Kills = new RecordInfo(array);
			this.Headshots = new RecordInfo(array);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001B308 File Offset: 0x00019508
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

		// Token: 0x060005FD RID: 1533 RVA: 0x0001B340 File Offset: 0x00019540
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

		// Token: 0x060005FE RID: 1534 RVA: 0x00005582 File Offset: 0x00003782
		public void SetBestExp(SlotModel Slot)
		{
			if (Slot.Exp <= this.Exp.RecordValue)
			{
				return;
			}
			this.Exp.PlayerId = Slot.PlayerId;
			this.Exp.RecordValue = Slot.Exp;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000055BA File Offset: 0x000037BA
		public void SetBestHeadshot(SlotModel Slot)
		{
			if (Slot.AllHeadshots <= this.Headshots.RecordValue)
			{
				return;
			}
			this.Headshots.PlayerId = Slot.PlayerId;
			this.Headshots.RecordValue = Slot.AllHeadshots;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000055F2 File Offset: 0x000037F2
		public void SetBestKills(SlotModel Slot)
		{
			if (Slot.AllKills <= this.Kills.RecordValue)
			{
				return;
			}
			this.Kills.PlayerId = Slot.PlayerId;
			this.Kills.RecordValue = Slot.AllKills;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001BA4C File Offset: 0x00019C4C
		public void SetBestWins(PlayerStatistic Stat, SlotModel Slot, bool WonTheMatch)
		{
			if (!WonTheMatch)
			{
				return;
			}
			string text = "player_stat_clans";
			string text2 = "clan_match_wins";
			StatisticClan clan = Stat.Clan;
			int num = clan.MatchWins + 1;
			clan.MatchWins = num;
			ComDiv.UpdateDB(text, text2, num, "owner_id", Slot.PlayerId);
			if (Stat.Clan.MatchWins <= this.Wins.RecordValue)
			{
				return;
			}
			this.Wins.PlayerId = Slot.PlayerId;
			this.Wins.RecordValue = Stat.Clan.MatchWins;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001BAD8 File Offset: 0x00019CD8
		public void SetBestParticipation(PlayerStatistic Stat, SlotModel Slot)
		{
			string text = "player_stat_clans";
			string text2 = "clan_matches";
			StatisticClan clan = Stat.Clan;
			int num = clan.Matches + 1;
			clan.Matches = num;
			ComDiv.UpdateDB(text, text2, num, "owner_id", Slot.PlayerId);
			if (Stat.Clan.Matches <= this.Participation.RecordValue)
			{
				return;
			}
			this.Participation.PlayerId = Slot.PlayerId;
			this.Participation.RecordValue = Stat.Clan.Matches;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00002116 File Offset: 0x00000316
		public ClanBestPlayers()
		{
		}

		// Token: 0x0400027B RID: 635
		[CompilerGenerated]
		private RecordInfo recordInfo_0;

		// Token: 0x0400027C RID: 636
		[CompilerGenerated]
		private RecordInfo recordInfo_1;

		// Token: 0x0400027D RID: 637
		[CompilerGenerated]
		private RecordInfo recordInfo_2;

		// Token: 0x0400027E RID: 638
		[CompilerGenerated]
		private RecordInfo recordInfo_3;

		// Token: 0x0400027F RID: 639
		[CompilerGenerated]
		private RecordInfo recordInfo_4;
	}
}
