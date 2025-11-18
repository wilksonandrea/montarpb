using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1F RID: 2591
	[Guid("346dd6e7-976e-4bc3-815d-ece243bc0f33")]
	[ComImport]
	internal interface IBindableVectorView : IBindableIterable
	{
		// Token: 0x060065F6 RID: 26102
		object GetAt(uint index);

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060065F7 RID: 26103
		uint Size { get; }

		// Token: 0x060065F8 RID: 26104
		bool IndexOf(object value, out uint index);
	}
}
