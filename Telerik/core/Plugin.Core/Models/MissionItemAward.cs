using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MissionItemAward
	{
		public ItemsModel Item
		{
			get;
			set;
		}

		public int MissionId
		{
			get;
			set;
		}

		public MissionItemAward()
		{
		}
	}
}