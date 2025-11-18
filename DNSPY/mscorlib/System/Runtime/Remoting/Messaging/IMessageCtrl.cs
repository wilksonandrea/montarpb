using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085A RID: 2138
	[ComVisible(true)]
	public interface IMessageCtrl
	{
		// Token: 0x06005A84 RID: 23172
		[SecurityCritical]
		void Cancel(int msToCancel);
	}
}
