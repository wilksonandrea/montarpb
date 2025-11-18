using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerReport
	{
		public long OwnerId
		{
			get;
			set;
		}

		public int ReportedCount
		{
			get;
			set;
		}

		public int TicketCount
		{
			get;
			set;
		}

		public PlayerReport()
		{
		}
	}
}