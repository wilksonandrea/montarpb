using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E4 RID: 2532
	[Guid("00000035-0000-0000-C000-000000000046")]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IActivationFactory
	{
		// Token: 0x06006487 RID: 25735
		[__DynamicallyInvokable]
		object ActivateInstance();
	}
}
