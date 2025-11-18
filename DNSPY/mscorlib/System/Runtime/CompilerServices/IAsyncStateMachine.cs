using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F5 RID: 2293
	[__DynamicallyInvokable]
	public interface IAsyncStateMachine
	{
		// Token: 0x06005E3E RID: 24126
		[__DynamicallyInvokable]
		void MoveNext();

		// Token: 0x06005E3F RID: 24127
		[__DynamicallyInvokable]
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}
