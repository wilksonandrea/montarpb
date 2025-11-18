using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class SlotMatch
	{
		public int Id
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public SlotMatchState State
		{
			get;
			set;
		}

		public SlotMatch(int int_1)
		{
			this.Id = int_1;
		}
	}
}