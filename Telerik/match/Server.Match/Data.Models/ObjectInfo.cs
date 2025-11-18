using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class ObjectInfo
	{
		public AnimModel Animation
		{
			get;
			set;
		}

		public int DestroyState
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int Life
		{
			get;
			set;
		}

		public ObjectModel Model
		{
			get;
			set;
		}

		public DateTime UseDate
		{
			get;
			set;
		}

		public ObjectInfo(int int_3)
		{
			this.Id = int_3;
			this.Life = 100;
		}
	}
}