using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004E6 RID: 1254
	internal interface IAsyncLocal
	{
		// Token: 0x06003B80 RID: 15232
		[SecurityCritical]
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}
