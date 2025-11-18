using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class BattlePassModel
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class11
	{
		public static readonly Class11 _003C_003E9 = new Class11();

		public static Func<PassBoxModel, int> _003C_003E9__33_0;

		public static Func<PassBoxModel, int> _003C_003E9__33_3;

		public static Func<PassBoxModel, int> _003C_003E9__34_0;

		internal int method_0(PassBoxModel passBoxModel_0)
		{
			return passBoxModel_0.Card;
		}

		internal int method_1(PassBoxModel passBoxModel_0)
		{
			return passBoxModel_0.RequiredPoints;
		}

		internal int method_2(PassBoxModel passBoxModel_0)
		{
			return passBoxModel_0.RequiredPoints;
		}
	}

	[CompilerGenerated]
	private sealed class Class12
	{
		public int int_0;

		internal bool method_0(PassBoxModel passBoxModel_0)
		{
			return passBoxModel_0.Card == int_0;
		}

		internal bool method_1(PassBoxModel passBoxModel_0)
		{
			return passBoxModel_0.Card == int_0;
		}

		internal bool method_2(PassBoxModel passBoxModel_0)
		{
			return passBoxModel_0.Card == int_0;
		}
	}

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private uint uint_0;

	[CompilerGenerated]
	private uint uint_1;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private List<PassBoxModel> list_0;

	public int Id
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public int MaxDailyPoints
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public uint BeginDate
	{
		[CompilerGenerated]
		get
		{
			return uint_0;
		}
		[CompilerGenerated]
		set
		{
			uint_0 = value;
		}
	}

	public uint EndedDate
	{
		[CompilerGenerated]
		get
		{
			return uint_1;
		}
		[CompilerGenerated]
		set
		{
			uint_1 = value;
		}
	}

	public bool Enable
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public List<PassBoxModel> Cards
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public BattlePassModel()
	{
		Name = "";
	}

	public bool SeasonIsEnabled()
	{
		uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
		if (BeginDate <= num)
		{
			return num < EndedDate;
		}
		return false;
	}

	public List<PassItemModel> GetReward(int Level, bool IsPremium)
	{
		List<PassItemModel> list = new List<PassItemModel>();
		if (IsPremium)
		{
			list.Add(Cards[Level].Normal);
			list.Add(Cards[Level].PremiumA);
			list.Add(Cards[Level].PremiumB);
		}
		else
		{
			list.Add(Cards[Level].Normal);
		}
		return list;
	}

	public void SetBoxCounts()
	{
		for (int i = 0; i < 99; i++)
		{
			Cards[i].SetCount();
		}
	}

	public int GetCardCount()
	{
		int num = 0;
		foreach (PassBoxModel card in Cards)
		{
			if (card.RewardCount > 0)
			{
				num++;
			}
		}
		return num;
	}

	public (int, int, int, int) GetLevelProgression(int PassPoints)
	{
		int num = 0;
		int int_0 = 0;
		int num2 = 0;
		int item = 0;
		int item2 = 0;
		List<PassBoxModel> list = Cards.OrderBy((PassBoxModel passBoxModel_0) => passBoxModel_0.Card).ToList();
		foreach (PassBoxModel item3 in list)
		{
			if (PassPoints >= item3.RequiredPoints)
			{
				num = item3.Card;
				num2 = item3.RequiredPoints;
				continue;
			}
			int_0 = item3.Card;
			item = item3.RequiredPoints - (PassPoints - num2);
			item2 = num2 + item3.RequiredPoints;
			break;
		}
		if (int_0 == 0 && list.Any())
		{
			int_0 = list.Last().Card + 1;
			item = 0;
			item2 = num2;
		}
		else if (num > 0 && int_0 == 0)
		{
			int_0 = num + 1;
		}
		else if (num == 0 && int_0 == 0 && list.Any())
		{
			int_0 = list.First().Card;
			item = list.First().RequiredPoints - PassPoints;
			item2 = list.First().RequiredPoints;
		}
		else if (int_0 > 0)
		{
			int num3 = list.FindIndex((PassBoxModel passBoxModel_0) => passBoxModel_0.Card == int_0);
			if (num3 >= 0)
			{
				int requiredPoints = list[num3].RequiredPoints;
				int num4 = 0;
				if (num3 > 0)
				{
					num4 = list.Take(num3).Sum((PassBoxModel passBoxModel_0) => passBoxModel_0.RequiredPoints);
				}
				item = requiredPoints - (PassPoints - num4);
				item2 = num4 + requiredPoints;
			}
			else if (num > 0 && !list.Any((PassBoxModel passBoxModel_0) => passBoxModel_0.Card == int_0))
			{
				item = 0;
				item2 = num2;
			}
		}
		else if (num > 0 && int_0 == 0 && !list.Any())
		{
			item = 0;
			item2 = num2;
		}
		else if (num == 0 && int_0 > 0)
		{
			int num5 = list.FindIndex((PassBoxModel passBoxModel_0) => passBoxModel_0.Card == int_0);
			if (num5 >= 0)
			{
				item = list[num5].RequiredPoints - PassPoints;
				item2 = list[num5].RequiredPoints;
			}
		}
		return (num, int_0, item, item2);
	}

	public bool IsCompleted(int PassPoints)
	{
		if (Cards != null && Cards.Any())
		{
			int num = Cards.Sum((PassBoxModel passBoxModel_0) => passBoxModel_0.RequiredPoints);
			return PassPoints >= num;
		}
		return true;
	}
}
