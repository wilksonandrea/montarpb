using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F6 RID: 2294
	[__DynamicallyInvokable]
	public interface INotifyCompletion
	{
		// Token: 0x06005E40 RID: 24128
		[__DynamicallyInvokable]
		void OnCompleted(Action continuation);
	}
}
