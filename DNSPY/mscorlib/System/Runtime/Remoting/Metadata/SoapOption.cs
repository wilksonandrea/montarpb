using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D7 RID: 2007
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SoapOption
	{
		// Token: 0x040027D3 RID: 10195
		None = 0,
		// Token: 0x040027D4 RID: 10196
		AlwaysIncludeTypes = 1,
		// Token: 0x040027D5 RID: 10197
		XsdString = 2,
		// Token: 0x040027D6 RID: 10198
		EmbedAll = 4,
		// Token: 0x040027D7 RID: 10199
		Option1 = 8,
		// Token: 0x040027D8 RID: 10200
		Option2 = 16
	}
}
