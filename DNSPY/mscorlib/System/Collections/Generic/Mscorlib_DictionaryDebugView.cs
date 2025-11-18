using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CE RID: 1230
	internal sealed class Mscorlib_DictionaryDebugView<K, V>
	{
		// Token: 0x06003AC3 RID: 15043 RVA: 0x000DF790 File Offset: 0x000DD990
		public Mscorlib_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			this.dict = dictionary;
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000DF7A8 File Offset: 0x000DD9A8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001957 RID: 6487
		private IDictionary<K, V> dict;
	}
}
