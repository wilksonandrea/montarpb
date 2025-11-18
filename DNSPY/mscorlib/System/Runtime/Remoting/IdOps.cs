using System;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B4 RID: 1972
	internal struct IdOps
	{
		// Token: 0x06005575 RID: 21877 RVA: 0x0012F4DE File Offset: 0x0012D6DE
		internal static bool bStrongIdentity(int flags)
		{
			return (flags & 2) != 0;
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x0012F4E6 File Offset: 0x0012D6E6
		internal static bool bIsInitializing(int flags)
		{
			return (flags & 4) != 0;
		}

		// Token: 0x04002761 RID: 10081
		internal const int None = 0;

		// Token: 0x04002762 RID: 10082
		internal const int GenerateURI = 1;

		// Token: 0x04002763 RID: 10083
		internal const int StrongIdentity = 2;

		// Token: 0x04002764 RID: 10084
		internal const int IsInitializing = 4;
	}
}
