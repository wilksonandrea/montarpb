using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008ED RID: 2285
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06005E0D RID: 24077 RVA: 0x0014A401 File Offset: 0x00148601
		[__DynamicallyInvokable]
		public IteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
