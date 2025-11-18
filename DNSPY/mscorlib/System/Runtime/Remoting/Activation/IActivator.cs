using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089B RID: 2203
	[ComVisible(true)]
	public interface IActivator
	{
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005D2F RID: 23855
		// (set) Token: 0x06005D30 RID: 23856
		IActivator NextActivator
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x06005D31 RID: 23857
		[SecurityCritical]
		IConstructionReturnMessage Activate(IConstructionCallMessage msg);

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005D32 RID: 23858
		ActivatorLevel Level
		{
			[SecurityCritical]
			get;
		}
	}
}
