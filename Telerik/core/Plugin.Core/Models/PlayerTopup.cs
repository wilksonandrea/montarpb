using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerTopup
	{
		public int GoodsId
		{
			get;
			set;
		}

		public long ObjectId
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public PlayerTopup()
		{
		}
	}
}