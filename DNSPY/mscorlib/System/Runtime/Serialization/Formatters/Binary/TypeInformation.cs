using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A7 RID: 1959
	internal sealed class TypeInformation
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x0012DE82 File Offset: 0x0012C082
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x0012DE8A File Offset: 0x0012C08A
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x0012DE92 File Offset: 0x0012C092
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0012DE9A File Offset: 0x0012C09A
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x04002716 RID: 10006
		private string fullTypeName;

		// Token: 0x04002717 RID: 10007
		private string assemblyString;

		// Token: 0x04002718 RID: 10008
		private bool hasTypeForwardedFrom;
	}
}
