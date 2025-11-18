using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DF RID: 1759
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipDataEntry
	{
		// Token: 0x06005099 RID: 20633 RVA: 0x0011DB15 File Offset: 0x0011BD15
		public CategoryMembershipDataEntry()
		{
		}

		// Token: 0x04002339 RID: 9017
		public uint index;

		// Token: 0x0400233A RID: 9018
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;

		// Token: 0x0400233B RID: 9019
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;
	}
}
