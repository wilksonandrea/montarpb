using System;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000806 RID: 2054
	internal sealed class __TransparentProxy
	{
		// Token: 0x06005874 RID: 22644 RVA: 0x00137CDD File Offset: 0x00135EDD
		private __TransparentProxy()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Constructor"));
		}

		// Token: 0x04002859 RID: 10329
		[SecurityCritical]
		private RealProxy _rp;

		// Token: 0x0400285A RID: 10330
		private object _stubData;

		// Token: 0x0400285B RID: 10331
		private IntPtr _pMT;

		// Token: 0x0400285C RID: 10332
		private IntPtr _pInterfaceMT;

		// Token: 0x0400285D RID: 10333
		private IntPtr _stub;
	}
}
