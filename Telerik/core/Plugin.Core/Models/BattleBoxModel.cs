using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class BattleBoxModel
	{
		public int CouponId
		{
			get;
			set;
		}

		public List<int> Goods
		{
			get;
			set;
		}

		public List<BattleBoxItem> Items
		{
			get;
			set;
		}

		public List<double> Probabilities
		{
			get;
			set;
		}

		public int RequireTags
		{
			get;
			set;
		}

		public BattleBoxModel()
		{
		}

		public T GetItemWithProbabilities<T>(List<T> Items, List<double> Probabilities)
		{
			if (Items == null || Items.Count == 0 || Probabilities == null || Probabilities.Count == 0 || Items.Count != Probabilities.Count)
			{
				CLogger.Print("Battle Box Item List Is Not Valid!", LoggerType.Warning, null);
			}
			double num = (new Random()).NextDouble();
			double ıtem = 0;
			for (int i = 0; i < Items.Count; i++)
			{
				ıtem = ıtem + Probabilities[i] / 100;
				if (num < ıtem)
				{
					return Items[i];
				}
			}
			return Items[Items.Count - 1];
		}

		public void InitItemPercentages()
		{
			this.Goods = new List<int>();
			this.Probabilities = new List<double>();
			foreach (BattleBoxItem ıtem in this.Items)
			{
				this.Goods.Add(ıtem.GoodsId);
				this.Probabilities.Add((double)ıtem.Percent);
			}
		}
	}
}