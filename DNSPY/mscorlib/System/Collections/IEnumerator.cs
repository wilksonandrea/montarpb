using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200049F RID: 1183
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IEnumerator
	{
		// Token: 0x060038BF RID: 14527
		[__DynamicallyInvokable]
		bool MoveNext();

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060038C0 RID: 14528
		[__DynamicallyInvokable]
		object Current
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x060038C1 RID: 14529
		[__DynamicallyInvokable]
		void Reset();
	}
}
