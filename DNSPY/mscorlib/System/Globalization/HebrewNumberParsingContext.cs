using System;

namespace System.Globalization
{
	// Token: 0x020003DD RID: 989
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x060032E2 RID: 13026 RVA: 0x000C418B File Offset: 0x000C238B
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x0400168C RID: 5772
		internal HebrewNumber.HS state;

		// Token: 0x0400168D RID: 5773
		internal int result;
	}
}
