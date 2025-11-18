using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class ClanInvite
	{
		public int Id
		{
			get;
			set;
		}

		public uint InviteDate
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public ClanInvite()
		{
		}
	}
}