using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E5 RID: 1765
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipEntry
	{
		// Token: 0x060050A2 RID: 20642 RVA: 0x0011DB25 File Offset: 0x0011BD25
		public CategoryMembershipEntry()
		{
		}

		// Token: 0x04002343 RID: 9027
		public IDefinitionIdentity Identity;

		// Token: 0x04002344 RID: 9028
		public ISection SubcategoryMembership;
	}
}
