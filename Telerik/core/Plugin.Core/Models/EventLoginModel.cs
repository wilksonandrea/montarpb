using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class EventLoginModel
	{
		public uint BeginDate
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public uint EndedDate
		{
			get;
			set;
		}

		public List<int> Goods
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public bool Period
		{
			get;
			set;
		}

		public bool Priority
		{
			get;
			set;
		}

		public EventLoginModel()
		{
			this.Name = "";
			this.Description = "";
		}

		public bool EventIsEnabled()
		{
			uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			if (this.BeginDate > uInt32)
			{
				return false;
			}
			return uInt32 < this.EndedDate;
		}
	}
}