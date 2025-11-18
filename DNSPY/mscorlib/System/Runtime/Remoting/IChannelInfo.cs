using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B8 RID: 1976
	[ComVisible(true)]
	public interface IChannelInfo
	{
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600558E RID: 21902
		// (set) Token: 0x0600558F RID: 21903
		object[] ChannelData
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
