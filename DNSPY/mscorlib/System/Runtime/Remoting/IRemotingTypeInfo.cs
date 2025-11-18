using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B7 RID: 1975
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600558B RID: 21899
		// (set) Token: 0x0600558C RID: 21900
		string TypeName
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x0600558D RID: 21901
		[SecurityCritical]
		bool CanCastTo(Type fromType, object o);
	}
}
