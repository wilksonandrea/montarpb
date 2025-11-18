using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class RandomBoxItem
	{
		public int GoodsId
		{
			get;
			set;
		}

		public int Index
		{
			get;
			set;
		}

		public int Percent
		{
			get;
			set;
		}

		public bool Special
		{
			get;
			set;
		}

		public RandomBoxItem()
		{
		}
	}
}