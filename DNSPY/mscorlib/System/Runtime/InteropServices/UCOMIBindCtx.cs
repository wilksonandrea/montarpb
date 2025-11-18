using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097D RID: 2429
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IBindCtx instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000e-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIBindCtx
	{
		// Token: 0x0600627D RID: 25213
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x0600627E RID: 25214
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x0600627F RID: 25215
		void ReleaseBoundObjects();

		// Token: 0x06006280 RID: 25216
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x06006281 RID: 25217
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x06006282 RID: 25218
		void GetRunningObjectTable(out UCOMIRunningObjectTable pprot);

		// Token: 0x06006283 RID: 25219
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06006284 RID: 25220
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x06006285 RID: 25221
		void EnumObjectParam(out UCOMIEnumString ppenum);

		// Token: 0x06006286 RID: 25222
		void RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}
