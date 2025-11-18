using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MissionCardAwards
	{
		public int Card
		{
			get;
			set;
		}

		public int Ensign
		{
			get;
			set;
		}

		public int Exp
		{
			get;
			set;
		}

		public int Gold
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int Medal
		{
			get;
			set;
		}

		public int Ribbon
		{
			get;
			set;
		}

		public MissionCardAwards()
		{
		}

		public bool Unusable()
		{
			if (this.Ensign != 0 || this.Medal != 0 || this.Ribbon != 0 || this.Exp != 0)
			{
				return false;
			}
			return this.Gold == 0;
		}
	}
}