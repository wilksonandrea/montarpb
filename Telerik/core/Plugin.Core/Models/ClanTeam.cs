using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class ClanTeam
	{
		public int ClanId
		{
			get;
			set;
		}

		public int PlayersCT
		{
			get;
			set;
		}

		public int PlayersFR
		{
			get;
			set;
		}

		public ClanTeam()
		{
		}
	}
}