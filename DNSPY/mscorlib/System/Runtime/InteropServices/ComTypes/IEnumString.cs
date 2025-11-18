using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2F RID: 2607
	[Guid("00000101-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumString
	{
		// Token: 0x06006631 RID: 26161
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, IntPtr pceltFetched);

		// Token: 0x06006632 RID: 26162
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006633 RID: 26163
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x06006634 RID: 26164
		[__DynamicallyInvokable]
		void Clone(out IEnumString ppenum);
	}
}
