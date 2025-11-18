using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AC RID: 1196
	internal sealed class SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x0600392A RID: 14634 RVA: 0x000DA95C File Offset: 0x000D8B5C
		public SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_collection = collection;
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x000DA979 File Offset: 0x000D8B79
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_collection.ToArray();
			}
		}

		// Token: 0x04001914 RID: 6420
		private IProducerConsumerCollection<T> m_collection;
	}
}
