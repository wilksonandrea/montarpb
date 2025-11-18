using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A28 RID: 2600
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IConnectionPoint
	{
		// Token: 0x0600661C RID: 26140
		[__DynamicallyInvokable]
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x0600661D RID: 26141
		[__DynamicallyInvokable]
		void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

		// Token: 0x0600661E RID: 26142
		[__DynamicallyInvokable]
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x0600661F RID: 26143
		[__DynamicallyInvokable]
		void Unadvise(int dwCookie);

		// Token: 0x06006620 RID: 26144
		[__DynamicallyInvokable]
		void EnumConnections(out IEnumConnections ppEnum);
	}
}
