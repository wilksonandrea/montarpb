using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class InternetCafe
	{
		public int BasicExp
		{
			get;
			set;
		}

		public int BasicGold
		{
			get;
			set;
		}

		public int ConfigId
		{
			get;
			set;
		}

		public int PremiumExp
		{
			get;
			set;
		}

		public int PremiumGold
		{
			get;
			set;
		}

		public InternetCafe(int int_5)
		{
			this.ConfigId = int_5;
		}
	}
}