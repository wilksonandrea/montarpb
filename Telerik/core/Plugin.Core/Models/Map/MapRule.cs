using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models.Map
{
	public class MapRule
	{
		public int Conditions
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

		public int Rule
		{
			get;
			set;
		}

		public int StageOptions
		{
			get;
			set;
		}

		public MapRule()
		{
			this.Name = "";
		}
	}
}