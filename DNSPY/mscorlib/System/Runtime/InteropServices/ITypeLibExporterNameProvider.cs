using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000974 RID: 2420
	[Guid("FA1F3615-ACB9-486d-9EAC-1BEF87E36B09")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibExporterNameProvider
	{
		// Token: 0x06006243 RID: 25155
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		string[] GetNames();
	}
}
