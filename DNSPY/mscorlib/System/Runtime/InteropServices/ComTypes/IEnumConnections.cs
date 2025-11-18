using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2D RID: 2605
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumConnections
	{
		// Token: 0x06006629 RID: 26153
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, IntPtr pceltFetched);

		// Token: 0x0600662A RID: 26154
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600662B RID: 26155
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x0600662C RID: 26156
		[__DynamicallyInvokable]
		void Clone(out IEnumConnections ppenum);
	}
}
