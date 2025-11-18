using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F7 RID: 1783
	[StructLayout(LayoutKind.Sequential)]
	internal class WindowClassEntry
	{
		// Token: 0x060050CD RID: 20685 RVA: 0x0011DBC4 File Offset: 0x0011BDC4
		public WindowClassEntry()
		{
		}

		// Token: 0x0400237E RID: 9086
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;

		// Token: 0x0400237F RID: 9087
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostDll;

		// Token: 0x04002380 RID: 9088
		public bool fVersioned;
	}
}
