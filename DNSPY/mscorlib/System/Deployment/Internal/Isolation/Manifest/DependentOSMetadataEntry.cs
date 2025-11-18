using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070C RID: 1804
	[StructLayout(LayoutKind.Sequential)]
	internal class DependentOSMetadataEntry
	{
		// Token: 0x060050F4 RID: 20724 RVA: 0x0011DBFC File Offset: 0x0011BDFC
		public DependentOSMetadataEntry()
		{
		}

		// Token: 0x040023B2 RID: 9138
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040023B3 RID: 9139
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x040023B4 RID: 9140
		public ushort MajorVersion;

		// Token: 0x040023B5 RID: 9141
		public ushort MinorVersion;

		// Token: 0x040023B6 RID: 9142
		public ushort BuildNumber;

		// Token: 0x040023B7 RID: 9143
		public byte ServicePackMajor;

		// Token: 0x040023B8 RID: 9144
		public byte ServicePackMinor;
	}
}
