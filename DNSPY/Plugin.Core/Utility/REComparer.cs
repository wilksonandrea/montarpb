using System;
using System.Collections.Generic;

namespace Plugin.Core.Utility
{
	// Token: 0x02000034 RID: 52
	public class REComparer : EqualityComparer<object>
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00002C34 File Offset: 0x00000E34
		public override bool Equals(object X, object Y)
		{
			return X == Y;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00002C3A File Offset: 0x00000E3A
		public override int GetHashCode(object OBJ)
		{
			if (OBJ == null)
			{
				return 0;
			}
			return OBJ.GetHashCode();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00002C47 File Offset: 0x00000E47
		public REComparer()
		{
		}
	}
}
