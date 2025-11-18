using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class BanHistory
	{
		public DateTime EndDate
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

		public string Reason
		{
			get;
			set;
		}

		public DateTime StartDate
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public BanHistory()
		{
			this.StartDate = DateTimeUtil.Now();
			this.Type = "";
			this.Value = "";
			this.Reason = "";
		}
	}
}