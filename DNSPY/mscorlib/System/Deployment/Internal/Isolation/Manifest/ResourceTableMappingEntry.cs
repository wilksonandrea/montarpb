using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006FA RID: 1786
	[StructLayout(LayoutKind.Sequential)]
	internal class ResourceTableMappingEntry
	{
		// Token: 0x060050D2 RID: 20690 RVA: 0x0011DBCC File Offset: 0x0011BDCC
		public ResourceTableMappingEntry()
		{
		}

		// Token: 0x04002384 RID: 9092
		[MarshalAs(UnmanagedType.LPWStr)]
		public string id;

		// Token: 0x04002385 RID: 9093
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FinalStringMapped;
	}
}
