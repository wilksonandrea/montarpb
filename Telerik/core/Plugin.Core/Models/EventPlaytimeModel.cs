using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class EventPlaytimeModel
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

		public List<int> Goods1
		{
			get;
			set;
		}

		public List<int> Goods2
		{
			get;
			set;
		}

		public List<int> Goods3
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int Minutes1
		{
			get;
			set;
		}

		public int Minutes2
		{
			get;
			set;
		}

		public int Minutes3
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

		public EventPlaytimeModel()
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