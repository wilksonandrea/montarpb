using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F3 RID: 2547
	[Guid("60D27C8D-5F61-4CCE-B751-690FAE66AA53")]
	[ComImport]
	internal interface IManagedActivationFactory
	{
		// Token: 0x060064C4 RID: 25796
		void RunClassConstructor();
	}
}
