using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A36 RID: 2614
	[Guid("00000010-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IRunningObjectTable
	{
		// Token: 0x06006663 RID: 26211
		[__DynamicallyInvokable]
		int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

		// Token: 0x06006664 RID: 26212
		[__DynamicallyInvokable]
		void Revoke(int dwRegister);

		// Token: 0x06006665 RID: 26213
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsRunning(IMoniker pmkObjectName);

		// Token: 0x06006666 RID: 26214
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x06006667 RID: 26215
		[__DynamicallyInvokable]
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x06006668 RID: 26216
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x06006669 RID: 26217
		[__DynamicallyInvokable]
		void EnumRunning(out IEnumMoniker ppenumMoniker);
	}
}
