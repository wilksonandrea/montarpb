using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098D RID: 2445
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IRunningObjectTable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00000010-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIRunningObjectTable
	{
		// Token: 0x060062D0 RID: 25296
		void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, UCOMIMoniker pmkObjectName, out int pdwRegister);

		// Token: 0x060062D1 RID: 25297
		void Revoke(int dwRegister);

		// Token: 0x060062D2 RID: 25298
		void IsRunning(UCOMIMoniker pmkObjectName);

		// Token: 0x060062D3 RID: 25299
		void GetObject(UCOMIMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x060062D4 RID: 25300
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x060062D5 RID: 25301
		void GetTimeOfLastChange(UCOMIMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x060062D6 RID: 25302
		void EnumRunning(out UCOMIEnumMoniker ppenumMoniker);
	}
}
