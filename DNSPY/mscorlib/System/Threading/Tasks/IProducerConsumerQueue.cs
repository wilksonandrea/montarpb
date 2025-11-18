using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x02000582 RID: 1410
	internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06004265 RID: 16997
		void Enqueue(T item);

		// Token: 0x06004266 RID: 16998
		bool TryDequeue(out T result);

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06004267 RID: 16999
		bool IsEmpty { get; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06004268 RID: 17000
		int Count { get; }

		// Token: 0x06004269 RID: 17001
		int GetCountSafe(object syncObj);
	}
}
