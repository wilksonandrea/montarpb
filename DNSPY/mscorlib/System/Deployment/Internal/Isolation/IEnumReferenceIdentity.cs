using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000695 RID: 1685
	[Guid("b30352cf-23da-4577-9b3f-b4e6573be53b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumReferenceIdentity
	{
		// Token: 0x06004F98 RID: 20376
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IReferenceIdentity[] ReferenceIdentity);

		// Token: 0x06004F99 RID: 20377
		[SecurityCritical]
		void Skip(uint celt);

		// Token: 0x06004F9A RID: 20378
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F9B RID: 20379
		[SecurityCritical]
		IEnumReferenceIdentity Clone();
	}
}
