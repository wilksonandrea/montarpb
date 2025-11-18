using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MissionStore
	{
		public bool Enable
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int ItemId
		{
			get;
			set;
		}

		public MissionStore()
		{
		}
	}
}