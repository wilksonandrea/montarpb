using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x02000583 RID: 1411
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600426A RID: 17002 RVA: 0x000F707B File Offset: 0x000F527B
		void IProducerConsumerQueue<T>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000F7084 File Offset: 0x000F5284
		bool IProducerConsumerQueue<T>.TryDequeue(out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x000F708D File Offset: 0x000F528D
		bool IProducerConsumerQueue<T>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x000F7095 File Offset: 0x000F5295
		int IProducerConsumerQueue<T>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000F709D File Offset: 0x000F529D
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			return base.Count;
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000F70A5 File Offset: 0x000F52A5
		public MultiProducerMultiConsumerQueue()
		{
		}
	}
}
