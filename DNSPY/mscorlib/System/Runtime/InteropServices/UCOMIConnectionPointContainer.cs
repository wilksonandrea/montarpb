using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097E RID: 2430
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPointContainer instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPointContainer
	{
		// Token: 0x06006287 RID: 25223
		void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

		// Token: 0x06006288 RID: 25224
		void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
	}
}
