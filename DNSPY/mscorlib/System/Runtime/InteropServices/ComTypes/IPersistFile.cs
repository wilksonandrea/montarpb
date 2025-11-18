using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A34 RID: 2612
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IPersistFile
	{
		// Token: 0x06006651 RID: 26193
		[__DynamicallyInvokable]
		void GetClassID(out Guid pClassID);

		// Token: 0x06006652 RID: 26194
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsDirty();

		// Token: 0x06006653 RID: 26195
		[__DynamicallyInvokable]
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x06006654 RID: 26196
		[__DynamicallyInvokable]
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x06006655 RID: 26197
		[__DynamicallyInvokable]
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x06006656 RID: 26198
		[__DynamicallyInvokable]
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
