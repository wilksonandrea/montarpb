using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E7 RID: 1767
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("97FDCA77-B6F2-4718-A1EB-29D0AECE9C03")]
	[ComImport]
	internal interface ICategoryMembershipEntry
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060050A3 RID: 20643
		CategoryMembershipEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060050A4 RID: 20644
		IDefinitionIdentity Identity
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060050A5 RID: 20645
		ISection SubcategoryMembership
		{
			[SecurityCritical]
			get;
		}
	}
}
