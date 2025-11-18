using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000706 RID: 1798
	[StructLayout(LayoutKind.Sequential)]
	internal class DescriptionMetadataEntry
	{
		// Token: 0x060050E5 RID: 20709 RVA: 0x0011DBEC File Offset: 0x0011BDEC
		public DescriptionMetadataEntry()
		{
		}

		// Token: 0x0400239A RID: 9114
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Publisher;

		// Token: 0x0400239B RID: 9115
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Product;

		// Token: 0x0400239C RID: 9116
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x0400239D RID: 9117
		[MarshalAs(UnmanagedType.LPWStr)]
		public string IconFile;

		// Token: 0x0400239E RID: 9118
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ErrorReportUrl;

		// Token: 0x0400239F RID: 9119
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SuiteName;
	}
}
