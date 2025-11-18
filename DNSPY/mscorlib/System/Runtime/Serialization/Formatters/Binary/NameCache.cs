using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079B RID: 1947
	internal sealed class NameCache
	{
		// Token: 0x06005459 RID: 21593 RVA: 0x00129084 File Offset: 0x00127284
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object obj;
			if (!NameCache.ht.TryGetValue(name, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x001290AA File Offset: 0x001272AA
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x001290BD File Offset: 0x001272BD
		public NameCache()
		{
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x001290C5 File Offset: 0x001272C5
		// Note: this type is marked as 'beforefieldinit'.
		static NameCache()
		{
		}

		// Token: 0x0400265E RID: 9822
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x0400265F RID: 9823
		private string name;
	}
}
