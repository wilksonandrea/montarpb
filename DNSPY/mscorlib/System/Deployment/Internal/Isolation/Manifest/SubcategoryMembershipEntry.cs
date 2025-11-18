using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E2 RID: 1762
	[StructLayout(LayoutKind.Sequential)]
	internal class SubcategoryMembershipEntry
	{
		// Token: 0x0600509E RID: 20638 RVA: 0x0011DB1D File Offset: 0x0011BD1D
		public SubcategoryMembershipEntry()
		{
		}

		// Token: 0x0400233F RID: 9023
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Subcategory;

		// Token: 0x04002340 RID: 9024
		public ISection CategoryMembershipData;
	}
}
