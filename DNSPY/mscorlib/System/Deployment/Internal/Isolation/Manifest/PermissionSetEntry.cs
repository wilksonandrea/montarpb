using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000700 RID: 1792
	[StructLayout(LayoutKind.Sequential)]
	internal class PermissionSetEntry
	{
		// Token: 0x060050DD RID: 20701 RVA: 0x0011DBDC File Offset: 0x0011BDDC
		public PermissionSetEntry()
		{
		}

		// Token: 0x04002392 RID: 9106
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Id;

		// Token: 0x04002393 RID: 9107
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XmlSegment;
	}
}
