using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200006C RID: 108
	public class BattlePassModel
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00004914 File Offset: 0x00002B14
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000491C File Offset: 0x00002B1C
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00004925 File Offset: 0x00002B25
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000492D File Offset: 0x00002B2D
		public int MaxDailyPoints
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00004936 File Offset: 0x00002B36
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000493E File Offset: 0x00002B3E
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00004947 File Offset: 0x00002B47
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000494F File Offset: 0x00002B4F
		public uint BeginDate
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00004958 File Offset: 0x00002B58
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00004960 File Offset: 0x00002B60
		public uint EndedDate
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00004969 File Offset: 0x00002B69
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00004971 File Offset: 0x00002B71
		public bool Enable
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000497A File Offset: 0x00002B7A
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00004982 File Offset: 0x00002B82
		public List<PassBoxModel> Cards
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

		// Token: 0x0600049A RID: 1178 RVA: 0x0000498B File Offset: 0x00002B8B
		public BattlePassModel()
		{
			this.Name = "";
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001B3A4 File Offset: 0x000195A4
		public bool SeasonIsEnabled()
		{
			uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
			return this.BeginDate <= num && num < this.EndedDate;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001B3E0 File Offset: 0x000195E0
		public List<PassItemModel> GetReward(int Level, bool IsPremium)
		{
			List<PassItemModel> list = new List<PassItemModel>();
			if (IsPremium)
			{
				list.Add(this.Cards[Level].Normal);
				list.Add(this.Cards[Level].PremiumA);
				list.Add(this.Cards[Level].PremiumB);
			}
			else
			{
				list.Add(this.Cards[Level].Normal);
			}
			return list;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001B458 File Offset: 0x00019658
		public void SetBoxCounts()
		{
			for (int i = 0; i < 99; i++)
			{
				this.Cards[i].SetCount();
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001B484 File Offset: 0x00019684
		public int GetCardCount()
		{
			int num = 0;
			using (List<PassBoxModel>.Enumerator enumerator = this.Cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.RewardCount > 0)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001B4E0 File Offset: 0x000196E0
		public ValueTuple<int, int, int, int> GetLevelProgression(int PassPoints)
		{
			BattlePassModel.Class12 @class = new BattlePassModel.Class12();
			int num = 0;
			@class.int_0 = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			List<PassBoxModel> list = this.Cards.OrderBy(new Func<PassBoxModel, int>(BattlePassModel.Class11.<>9.method_0)).ToList<PassBoxModel>();
			foreach (PassBoxModel passBoxModel in list)
			{
				if (PassPoints < passBoxModel.RequiredPoints)
				{
					@class.int_0 = passBoxModel.Card;
					num3 = passBoxModel.RequiredPoints - (PassPoints - num2);
					num4 = num2 + passBoxModel.RequiredPoints;
					break;
				}
				num = passBoxModel.Card;
				num2 = passBoxModel.RequiredPoints;
			}
			if (@class.int_0 == 0 && list.Any<PassBoxModel>())
			{
				@class.int_0 = list.Last<PassBoxModel>().Card + 1;
				num3 = 0;
				num4 = num2;
			}
			else if (num > 0 && @class.int_0 == 0)
			{
				@class.int_0 = num + 1;
			}
			else if (num == 0 && @class.int_0 == 0 && list.Any<PassBoxModel>())
			{
				@class.int_0 = list.First<PassBoxModel>().Card;
				num3 = list.First<PassBoxModel>().RequiredPoints - PassPoints;
				num4 = list.First<PassBoxModel>().RequiredPoints;
			}
			else if (@class.int_0 > 0)
			{
				int num5 = list.FindIndex(new Predicate<PassBoxModel>(@class.method_0));
				if (num5 >= 0)
				{
					int requiredPoints = list[num5].RequiredPoints;
					int num6 = 0;
					if (num5 > 0)
					{
						num6 = list.Take(num5).Sum(new Func<PassBoxModel, int>(BattlePassModel.Class11.<>9.method_1));
					}
					num3 = requiredPoints - (PassPoints - num6);
					num4 = num6 + requiredPoints;
				}
				else if (num > 0 && !list.Any(new Func<PassBoxModel, bool>(@class.method_1)))
				{
					num3 = 0;
					num4 = num2;
				}
			}
			else if (num > 0 && @class.int_0 == 0 && !list.Any<PassBoxModel>())
			{
				num3 = 0;
				num4 = num2;
			}
			else if (num == 0 && @class.int_0 > 0)
			{
				int num7 = list.FindIndex(new Predicate<PassBoxModel>(@class.method_2));
				if (num7 >= 0)
				{
					num3 = list[num7].RequiredPoints - PassPoints;
					num4 = list[num7].RequiredPoints;
				}
			}
			return new ValueTuple<int, int, int, int>(num, @class.int_0, num3, num4);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001B758 File Offset: 0x00019958
		public bool IsCompleted(int PassPoints)
		{
			if (this.Cards != null && this.Cards.Any<PassBoxModel>())
			{
				int num = this.Cards.Sum(new Func<PassBoxModel, int>(BattlePassModel.Class11.<>9.method_2));
				return PassPoints >= num;
			}
			return true;
		}

		// Token: 0x040001C2 RID: 450
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001C3 RID: 451
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040001C4 RID: 452
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040001C5 RID: 453
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x040001C6 RID: 454
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x040001C7 RID: 455
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x040001C8 RID: 456
		[CompilerGenerated]
		private List<PassBoxModel> list_0;

		// Token: 0x0200006D RID: 109
		[CompilerGenerated]
		[Serializable]
		private sealed class Class11
		{
			// Token: 0x060004A1 RID: 1185 RVA: 0x0000499E File Offset: 0x00002B9E
			// Note: this type is marked as 'beforefieldinit'.
			static Class11()
			{
			}

			// Token: 0x060004A2 RID: 1186 RVA: 0x00002116 File Offset: 0x00000316
			public Class11()
			{
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x000049AA File Offset: 0x00002BAA
			internal int method_0(PassBoxModel passBoxModel_0)
			{
				return passBoxModel_0.Card;
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x000049B2 File Offset: 0x00002BB2
			internal int method_1(PassBoxModel passBoxModel_0)
			{
				return passBoxModel_0.RequiredPoints;
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x000049B2 File Offset: 0x00002BB2
			internal int method_2(PassBoxModel passBoxModel_0)
			{
				return passBoxModel_0.RequiredPoints;
			}

			// Token: 0x040001C9 RID: 457
			public static readonly BattlePassModel.Class11 <>9 = new BattlePassModel.Class11();

			// Token: 0x040001CA RID: 458
			public static Func<PassBoxModel, int> <>9__33_0;

			// Token: 0x040001CB RID: 459
			public static Func<PassBoxModel, int> <>9__33_3;

			// Token: 0x040001CC RID: 460
			public static Func<PassBoxModel, int> <>9__34_0;
		}

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		private sealed class Class12
		{
			// Token: 0x060004A6 RID: 1190 RVA: 0x00002116 File Offset: 0x00000316
			public Class12()
			{
			}

			// Token: 0x060004A7 RID: 1191 RVA: 0x000049BA File Offset: 0x00002BBA
			internal bool method_0(PassBoxModel passBoxModel_0)
			{
				return passBoxModel_0.Card == this.int_0;
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x000049BA File Offset: 0x00002BBA
			internal bool method_1(PassBoxModel passBoxModel_0)
			{
				return passBoxModel_0.Card == this.int_0;
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x000049BA File Offset: 0x00002BBA
			internal bool method_2(PassBoxModel passBoxModel_0)
			{
				return passBoxModel_0.Card == this.int_0;
			}

			// Token: 0x040001CD RID: 461
			public int int_0;
		}
	}
}
