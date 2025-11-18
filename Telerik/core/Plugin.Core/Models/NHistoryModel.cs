using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class NHistoryModel
	{
		public uint ChangeDate
		{
			get;
			set;
		}

		public string Motive
		{
			get;
			set;
		}

		public string NewNick
		{
			get;
			set;
		}

		public long ObjectId
		{
			get;
			set;
		}

		public string OldNick
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public NHistoryModel()
		{
		}
	}
}