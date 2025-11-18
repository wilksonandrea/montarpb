using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A30 RID: 2608
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumVARIANT
	{
		// Token: 0x06006635 RID: 26165
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] object[] rgVar, IntPtr pceltFetched);

		// Token: 0x06006636 RID: 26166
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006637 RID: 26167
		[__DynamicallyInvokable]
		[PreserveSig]
		int Reset();

		// Token: 0x06006638 RID: 26168
		[__DynamicallyInvokable]
		IEnumVARIANT Clone();
	}
}
