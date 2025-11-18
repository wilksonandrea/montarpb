using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A27 RID: 2599
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IConnectionPointContainer
	{
		// Token: 0x0600661A RID: 26138
		[__DynamicallyInvokable]
		void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

		// Token: 0x0600661B RID: 26139
		[__DynamicallyInvokable]
		void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
	}
}
