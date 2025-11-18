using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PCCafeModel
	{
		public int ExpUp
		{
			get;
			set;
		}

		public int PointUp
		{
			get;
			set;
		}

		public SortedList<CafeEnum, List<ItemsModel>> Rewards
		{
			get;
			set;
		}

		public CafeEnum Type
		{
			get;
			set;
		}

		public PCCafeModel(CafeEnum cafeEnum_1)
		{
			this.Type = cafeEnum_1;
			this.PointUp = 0;
			this.ExpUp = 0;
		}
	}
}