using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CC RID: 1228
	internal sealed class Mscorlib_DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06003ABF RID: 15039 RVA: 0x000DF708 File Offset: 0x000DD908
		public Mscorlib_DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000DF720 File Offset: 0x000DD920
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001955 RID: 6485
		private ICollection<TKey> collection;
	}
}
