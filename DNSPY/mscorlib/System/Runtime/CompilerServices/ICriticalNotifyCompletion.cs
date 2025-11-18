using System;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F7 RID: 2295
	[__DynamicallyInvokable]
	public interface ICriticalNotifyCompletion : INotifyCompletion
	{
		// Token: 0x06005E41 RID: 24129
		[SecurityCritical]
		[__DynamicallyInvokable]
		void UnsafeOnCompleted(Action continuation);
	}
}
