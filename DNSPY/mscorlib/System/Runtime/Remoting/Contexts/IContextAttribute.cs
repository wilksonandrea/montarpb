using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200080E RID: 2062
	[ComVisible(true)]
	public interface IContextAttribute
	{
		// Token: 0x060058C5 RID: 22725
		[SecurityCritical]
		bool IsContextOK(Context ctx, IConstructionCallMessage msg);

		// Token: 0x060058C6 RID: 22726
		[SecurityCritical]
		void GetPropertiesForNewContext(IConstructionCallMessage msg);
	}
}
