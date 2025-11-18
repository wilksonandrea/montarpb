using System.Collections.Generic;

namespace Plugin.Core.Utility;

public class DBQuery
{
	private readonly List<string> list_0;

	private readonly List<object> list_1;

	public DBQuery()
	{
		list_0 = new List<string>();
		list_1 = new List<object>();
	}

	public void AddQuery(string table, object value)
	{
		list_0.Add(table);
		list_1.Add(value);
	}

	public string[] GetTables()
	{
		return list_0.ToArray();
	}

	public object[] GetValues()
	{
		return list_1.ToArray();
	}
}
