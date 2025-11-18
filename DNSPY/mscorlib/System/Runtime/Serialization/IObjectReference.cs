using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000736 RID: 1846
	[ComVisible(true)]
	public interface IObjectReference
	{
		// Token: 0x060051C6 RID: 20934
		[SecurityCritical]
		object GetRealObject(StreamingContext context);
	}
}
