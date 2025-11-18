using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B9 RID: 1977
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06005590 RID: 21904
		// (set) Token: 0x06005591 RID: 21905
		IMessageSink EnvoySinks
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
