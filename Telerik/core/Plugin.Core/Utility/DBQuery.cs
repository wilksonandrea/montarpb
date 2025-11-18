using System;
using System.Collections.Generic;

namespace Plugin.Core.Utility
{
	public class DBQuery
	{
		private readonly List<string> list_0;

		private readonly List<object> list_1;

		public DBQuery()
		{
			this.list_0 = new List<string>();
			this.list_1 = new List<object>();
		}

		public void AddQuery(string table, object value)
		{
			this.list_0.Add(table);
			this.list_1.Add(value);
		}

		public string[] GetTables()
		{
			return this.list_0.ToArray();
		}

		public object[] GetValues()
		{
			return this.list_1.ToArray();
		}
	}
}