using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097F RID: 2431
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPoint instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPoint
	{
		// Token: 0x06006289 RID: 25225
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x0600628A RID: 25226
		void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

		// Token: 0x0600628B RID: 25227
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x0600628C RID: 25228
		void Unadvise(int dwCookie);

		// Token: 0x0600628D RID: 25229
		void EnumConnections(out UCOMIEnumConnections ppEnum);
	}
}
