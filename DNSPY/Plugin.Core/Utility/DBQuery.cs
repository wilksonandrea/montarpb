using System;
using System.Collections.Generic;

namespace Plugin.Core.Utility
{
	// Token: 0x02000030 RID: 48
	public class DBQuery
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00002B25 File Offset: 0x00000D25
		public DBQuery()
		{
			this.list_0 = new List<string>();
			this.list_1 = new List<object>();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00002B43 File Offset: 0x00000D43
		public void AddQuery(string table, object value)
		{
			this.list_0.Add(table);
			this.list_1.Add(value);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00002B5D File Offset: 0x00000D5D
		public string[] GetTables()
		{
			return this.list_0.ToArray();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00002B6A File Offset: 0x00000D6A
		public object[] GetValues()
		{
			return this.list_1.ToArray();
		}

		// Token: 0x04000091 RID: 145
		private readonly List<string> list_0;

		// Token: 0x04000092 RID: 146
		private readonly List<object> list_1;
	}
}
