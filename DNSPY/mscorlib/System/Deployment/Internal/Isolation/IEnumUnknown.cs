using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000674 RID: 1652
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000100-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IEnumUnknown
	{
		// Token: 0x06004F2C RID: 20268
		[PreserveSig]
		int Next(uint celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] [Out] object[] rgelt, ref uint celtFetched);

		// Token: 0x06004F2D RID: 20269
		[PreserveSig]
		int Skip(uint celt);

		// Token: 0x06004F2E RID: 20270
		[PreserveSig]
		int Reset();

		// Token: 0x06004F2F RID: 20271
		[PreserveSig]
		int Clone(out IEnumUnknown enumUnknown);
	}
}
