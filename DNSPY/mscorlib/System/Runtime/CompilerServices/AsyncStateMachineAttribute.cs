using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008EF RID: 2287
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AsyncStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06005E10 RID: 24080 RVA: 0x0014A40A File Offset: 0x0014860A
		[__DynamicallyInvokable]
		public AsyncStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
