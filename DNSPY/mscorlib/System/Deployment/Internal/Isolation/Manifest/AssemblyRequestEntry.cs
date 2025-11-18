using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000703 RID: 1795
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyRequestEntry
	{
		// Token: 0x060050E1 RID: 20705 RVA: 0x0011DBE4 File Offset: 0x0011BDE4
		public AssemblyRequestEntry()
		{
		}

		// Token: 0x04002396 RID: 9110
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04002397 RID: 9111
		[MarshalAs(UnmanagedType.LPWStr)]
		public string permissionSetID;
	}
}
