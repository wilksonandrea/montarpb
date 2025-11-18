using System;
using System.Collections.Generic;

namespace Plugin.Core.Utility
{
	// Token: 0x02000037 RID: 55
	public class SafeList<T>
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00002C4F File Offset: 0x00000E4F
		public SafeList()
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public void Add(T Value)
		{
			object obj = this.object_0;
			lock (obj)
			{
				this.list_0.Add(Value);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00018C30 File Offset: 0x00016E30
		public void Clear()
		{
			object obj = this.object_0;
			lock (obj)
			{
				this.list_0.Clear();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00018C78 File Offset: 0x00016E78
		public bool Contains(T Value)
		{
			object obj = this.object_0;
			bool flag2;
			lock (obj)
			{
				flag2 = this.list_0.Contains(Value);
			}
			return flag2;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public int Count()
		{
			object obj = this.object_0;
			int count;
			lock (obj)
			{
				count = this.list_0.Count;
			}
			return count;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00018D08 File Offset: 0x00016F08
		public bool Remove(T Value)
		{
			object obj = this.object_0;
			bool flag2;
			lock (obj)
			{
				flag2 = this.list_0.Remove(Value);
			}
			return flag2;
		}

		// Token: 0x0400009A RID: 154
		private readonly List<T> list_0 = new List<T>();

		// Token: 0x0400009B RID: 155
		private readonly object object_0 = new object();
	}
}
