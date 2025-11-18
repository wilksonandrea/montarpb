using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200080F RID: 2063
	[ComVisible(true)]
	public interface IContextProperty
	{
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060058C7 RID: 22727
		string Name
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x060058C8 RID: 22728
		[SecurityCritical]
		bool IsNewContextOK(Context newCtx);

		// Token: 0x060058C9 RID: 22729
		[SecurityCritical]
		void Freeze(Context newContext);
	}
}
