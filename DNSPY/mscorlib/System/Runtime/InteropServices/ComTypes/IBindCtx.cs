using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A26 RID: 2598
	[Guid("0000000e-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IBindCtx
	{
		// Token: 0x06006610 RID: 26128
		[__DynamicallyInvokable]
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06006611 RID: 26129
		[__DynamicallyInvokable]
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06006612 RID: 26130
		[__DynamicallyInvokable]
		void ReleaseBoundObjects();

		// Token: 0x06006613 RID: 26131
		[__DynamicallyInvokable]
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x06006614 RID: 26132
		[__DynamicallyInvokable]
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x06006615 RID: 26133
		[__DynamicallyInvokable]
		void GetRunningObjectTable(out IRunningObjectTable pprot);

		// Token: 0x06006616 RID: 26134
		[__DynamicallyInvokable]
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06006617 RID: 26135
		[__DynamicallyInvokable]
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x06006618 RID: 26136
		[__DynamicallyInvokable]
		void EnumObjectParam(out IEnumString ppenum);

		// Token: 0x06006619 RID: 26137
		[__DynamicallyInvokable]
		[PreserveSig]
		int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}
