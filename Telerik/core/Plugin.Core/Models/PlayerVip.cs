using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerVip
	{
		public string Address
		{
			get;
			set;
		}

		public string Benefit
		{
			get;
			set;
		}

		public uint Expirate
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public PlayerVip()
		{
		}
	}
}