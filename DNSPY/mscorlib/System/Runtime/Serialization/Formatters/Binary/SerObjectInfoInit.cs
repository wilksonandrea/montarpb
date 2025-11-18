using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A5 RID: 1957
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x060054F0 RID: 21744 RVA: 0x0012DDFC File Offset: 0x0012BFFC
		public SerObjectInfoInit()
		{
		}

		// Token: 0x0400270D RID: 9997
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x0400270E RID: 9998
		internal int objectInfoIdCount = 1;

		// Token: 0x0400270F RID: 9999
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}
