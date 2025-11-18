using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070F RID: 1807
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworksMetadataEntry
	{
		// Token: 0x060050FD RID: 20733 RVA: 0x0011DC04 File Offset: 0x0011BE04
		public CompatibleFrameworksMetadataEntry()
		{
		}

		// Token: 0x040023C1 RID: 9153
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;
	}
}
