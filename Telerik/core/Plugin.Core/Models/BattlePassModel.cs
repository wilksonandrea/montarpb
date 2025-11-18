using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class BattlePassModel
	{
		public uint BeginDate
		{
			get;
			set;
		}

		public List<PassBoxModel> Cards
		{
			get;
			set;
		}

		public bool Enable
		{
			get;
			set;
		}

		public uint EndedDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int MaxDailyPoints
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public BattlePassModel()
		{
			this.Name = "";
		}

		public int GetCardCount()
		{
			int ınt32 = 0;
			foreach (PassBoxModel card in this.Cards)
			{
				if (card.RewardCount <= 0)
				{
					continue;
				}
				ınt32++;
			}
			return ınt32;
		}

		public ValueTuple<int, int, int, int> GetLevelProgression(int PassPoints)
		{
			int card = 0;
			int ınt32 = 0;
			int requiredPoints = 0;
			int passPoints = 0;
			int requiredPoints1 = 0;
			List<PassBoxModel> list = (
				from passBoxModel_0 in this.Cards
				orderby passBoxModel_0.Card
				select passBoxModel_0).ToList<PassBoxModel>();
			foreach (PassBoxModel passBoxModel in list)
			{
				if (PassPoints < passBoxModel.RequiredPoints)
				{
					ınt32 = passBoxModel.Card;
					passPoints = passBoxModel.RequiredPoints - (PassPoints - requiredPoints);
					requiredPoints1 = requiredPoints + passBoxModel.RequiredPoints;
					goto Label0;
				}
				else
				{
					card = passBoxModel.Card;
					requiredPoints = passBoxModel.RequiredPoints;
				}
			}
			if (ınt32 == 0 && list.Any<PassBoxModel>())
			{
				ınt32 = list.Last<PassBoxModel>().Card + 1;
				passPoints = 0;
				requiredPoints1 = requiredPoints;
			}
			else if (card > 0 && ınt32 == 0)
			{
				ınt32 = card + 1;
			}
			else if (card == 0 && ınt32 == 0 && list.Any<PassBoxModel>())
			{
				ınt32 = list.First<PassBoxModel>().Card;
				passPoints = list.First<PassBoxModel>().RequiredPoints - PassPoints;
				requiredPoints1 = list.First<PassBoxModel>().RequiredPoints;
			}
			else if (ınt32 > 0)
			{
				int ınt321 = list.FindIndex((PassBoxModel passBoxModel_0) => passBoxModel_0.Card == ınt32);
				if (ınt321 >= 0)
				{
					int requiredPoints2 = list[ınt321].RequiredPoints;
					int ınt322 = 0;
					if (ınt321 > 0)
					{
						ınt322 = list.Take<PassBoxModel>(ınt321).Sum<PassBoxModel>((PassBoxModel passBoxModel_0) => passBoxModel_0.RequiredPoints);
					}
					passPoints = requiredPoints2 - (PassPoints - ınt322);
					requiredPoints1 = ınt322 + requiredPoints2;
				}
				else if (card > 0 && !list.Any<PassBoxModel>((PassBoxModel passBoxModel_0) => passBoxModel_0.Card == ınt32))
				{
					passPoints = 0;
					requiredPoints1 = requiredPoints;
				}
			}
			else if (card > 0 && ınt32 == 0 && !list.Any<PassBoxModel>())
			{
				passPoints = 0;
				requiredPoints1 = requiredPoints;
			}
			else if (card == 0 && ınt32 > 0)
			{
				int ınt323 = list.FindIndex((PassBoxModel passBoxModel_0) => passBoxModel_0.Card == ınt32);
				if (ınt323 >= 0)
				{
					passPoints = list[ınt323].RequiredPoints - PassPoints;
					requiredPoints1 = list[ınt323].RequiredPoints;
				}
			}
			return new ValueTuple<int, int, int, int>(card, ınt32, passPoints, requiredPoints1);
		}

		public List<PassItemModel> GetReward(int Level, bool IsPremium)
		{
			List<PassItemModel> passItemModels = new List<PassItemModel>();
			if (!IsPremium)
			{
				passItemModels.Add(this.Cards[Level].Normal);
			}
			else
			{
				passItemModels.Add(this.Cards[Level].Normal);
				passItemModels.Add(this.Cards[Level].PremiumA);
				passItemModels.Add(this.Cards[Level].PremiumB);
			}
			return passItemModels;
		}

		public bool IsCompleted(int PassPoints)
		{
			if (this.Cards == null || !this.Cards.Any<PassBoxModel>())
			{
				return true;
			}
			return PassPoints >= this.Cards.Sum<PassBoxModel>((PassBoxModel passBoxModel_0) => passBoxModel_0.RequiredPoints);
		}

		public bool SeasonIsEnabled()
		{
			DateTime now = DateTime.Now;
			uint uInt32 = uint.Parse(now.ToString("yyMMddHHmm"));
			if (this.BeginDate > uInt32)
			{
				return false;
			}
			return uInt32 < this.EndedDate;
		}

		public void SetBoxCounts()
		{
			for (int i = 0; i < 99; i++)
			{
				this.Cards[i].SetCount();
			}
		}
	}
}