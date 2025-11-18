using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class EventBoostModel
	{
		public uint BeginDate
		{
			get;
			set;
		}

		public int BonusExp
		{
			get;
			set;
		}

		public int BonusGold
		{
			get;
			set;
		}

		public PortalBoostEvent BoostType
		{
			get;
			set;
		}

		public int BoostValue
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

		public int Percent
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

		public EventBoostModel()
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