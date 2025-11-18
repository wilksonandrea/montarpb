using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class TRuleModel
	{
		public List<int> BanIndexes
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public TRuleModel()
		{
		}
	}
}