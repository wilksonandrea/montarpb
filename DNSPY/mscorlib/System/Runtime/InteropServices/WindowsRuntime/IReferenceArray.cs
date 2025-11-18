using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0B RID: 2571
	[Guid("61c17707-2d65-11e0-9ae8-d48564015472")]
	[ComImport]
	internal interface IReferenceArray<T> : IPropertyValue
	{
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06006581 RID: 25985
		T[] Value { get; }
	}
}
