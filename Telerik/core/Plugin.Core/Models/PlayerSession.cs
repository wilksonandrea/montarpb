using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerSession
	{
		public long PlayerId
		{
			get;
			set;
		}

		public int SessionId
		{
			get;
			set;
		}

		public PlayerSession()
		{
		}
	}
}