using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006EE RID: 1774
	[StructLayout(LayoutKind.Sequential)]
	internal class CLRSurrogateEntry
	{
		// Token: 0x060050B4 RID: 20660 RVA: 0x0011DB3D File Offset: 0x0011BD3D
		public CLRSurrogateEntry()
		{
		}

		// Token: 0x0400235B RID: 9051
		public Guid Clsid;

		// Token: 0x0400235C RID: 9052
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x0400235D RID: 9053
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;
	}
}
