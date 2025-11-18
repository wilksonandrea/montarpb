using System;

namespace System.Threading
{
	// Token: 0x020004E8 RID: 1256
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x06003B88 RID: 15240
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x06003B89 RID: 15241
		IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent);
	}
}
