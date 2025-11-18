using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004D3 RID: 1235
	[TypeDependency("System.SZArrayHelper")]
	[__DynamicallyInvokable]
	public interface IEnumerable<out T> : IEnumerable
	{
		// Token: 0x06003AD7 RID: 15063
		[__DynamicallyInvokable]
		IEnumerator<T> GetEnumerator();
	}
}
