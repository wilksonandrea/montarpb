using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AB RID: 1195
	[__DynamicallyInvokable]
	public interface IProducerConsumerCollection<T> : IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06003926 RID: 14630
		[__DynamicallyInvokable]
		void CopyTo(T[] array, int index);

		// Token: 0x06003927 RID: 14631
		[__DynamicallyInvokable]
		bool TryAdd(T item);

		// Token: 0x06003928 RID: 14632
		[__DynamicallyInvokable]
		bool TryTake(out T item);

		// Token: 0x06003929 RID: 14633
		[__DynamicallyInvokable]
		T[] ToArray();
	}
}
