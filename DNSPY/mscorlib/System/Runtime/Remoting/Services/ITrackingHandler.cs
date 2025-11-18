using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x02000808 RID: 2056
	[ComVisible(true)]
	public interface ITrackingHandler
	{
		// Token: 0x06005879 RID: 22649
		[SecurityCritical]
		void MarshaledObject(object obj, ObjRef or);

		// Token: 0x0600587A RID: 22650
		[SecurityCritical]
		void UnmarshaledObject(object obj, ObjRef or);

		// Token: 0x0600587B RID: 22651
		[SecurityCritical]
		void DisconnectedObject(object obj);
	}
}
