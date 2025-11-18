using System;

namespace System.Collections.Generic
{
	// Token: 0x020004D4 RID: 1236
	[__DynamicallyInvokable]
	public interface IEnumerator<out T> : IDisposable, IEnumerator
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003AD8 RID: 15064
		[__DynamicallyInvokable]
		T Current
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
