using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MissionAwards
	{
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

		public int MasterMedal
		{
			get;
			set;
		}

		public MissionAwards(int int_4, int int_5, int int_6, int int_7)
		{
			this.Id = int_4;
			this.MasterMedal = int_5;
			this.Exp = int_6;
			this.Gold = int_7;
		}
	}
}