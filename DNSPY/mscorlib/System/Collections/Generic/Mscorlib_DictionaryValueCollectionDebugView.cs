using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CD RID: 1229
	internal sealed class Mscorlib_DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000DF74C File Offset: 0x000DD94C
		public Mscorlib_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000DF764 File Offset: 0x000DD964
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001956 RID: 6486
		private ICollection<TValue> collection;
	}
}
