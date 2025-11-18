using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2B RID: 2603
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumMoniker
	{
		// Token: 0x06006625 RID: 26149
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IMoniker[] rgelt, IntPtr pceltFetched);

		// Token: 0x06006626 RID: 26150
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006627 RID: 26151
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x06006628 RID: 26152
		[__DynamicallyInvokable]
		void Clone(out IEnumMoniker ppenum);
	}
}
