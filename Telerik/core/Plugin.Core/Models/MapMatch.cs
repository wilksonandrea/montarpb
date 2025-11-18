using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MapMatch
	{
		public int Id
		{
			get;
			set;
		}

		public int Limit
		{
			get;
			set;
		}

		public int Mode
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Tag
		{
			get;
			set;
		}

		public MapMatch(int int_4)
		{
			this.Mode = int_4;
		}
	}
}