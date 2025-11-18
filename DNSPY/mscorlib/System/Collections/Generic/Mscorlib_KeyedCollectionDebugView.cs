using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CF RID: 1231
	internal sealed class Mscorlib_KeyedCollectionDebugView<K, T>
	{
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000DF7D4 File Offset: 0x000DD9D4
		public Mscorlib_KeyedCollectionDebugView(KeyedCollection<K, T> keyedCollection)
		{
			if (keyedCollection == null)
			{
				throw new ArgumentNullException("keyedCollection");
			}
			this.kc = keyedCollection;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000DF7F4 File Offset: 0x000DD9F4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.kc.Count];
				this.kc.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001958 RID: 6488
		private KeyedCollection<K, T> kc;
	}
}
