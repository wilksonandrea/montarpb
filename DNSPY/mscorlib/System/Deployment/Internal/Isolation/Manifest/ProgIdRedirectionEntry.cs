using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006EB RID: 1771
	[StructLayout(LayoutKind.Sequential)]
	internal class ProgIdRedirectionEntry
	{
		// Token: 0x060050B0 RID: 20656 RVA: 0x0011DB35 File Offset: 0x0011BD35
		public ProgIdRedirectionEntry()
		{
		}

		// Token: 0x04002357 RID: 9047
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgId;

		// Token: 0x04002358 RID: 9048
		public Guid RedirectedGuid;
	}
}
