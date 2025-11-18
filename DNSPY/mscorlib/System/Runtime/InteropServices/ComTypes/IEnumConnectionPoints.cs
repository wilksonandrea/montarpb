using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2E RID: 2606
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumConnectionPoints
	{
		// Token: 0x0600662D RID: 26157
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IConnectionPoint[] rgelt, IntPtr pceltFetched);

		// Token: 0x0600662E RID: 26158
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600662F RID: 26159
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x06006630 RID: 26160
		[__DynamicallyInvokable]
		void Clone(out IEnumConnectionPoints ppenum);
	}
}
