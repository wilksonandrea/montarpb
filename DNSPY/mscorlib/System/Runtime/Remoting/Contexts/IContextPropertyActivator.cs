using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000810 RID: 2064
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		// Token: 0x060058CA RID: 22730
		[SecurityCritical]
		bool IsOKToActivate(IConstructionCallMessage msg);

		// Token: 0x060058CB RID: 22731
		[SecurityCritical]
		void CollectFromClientContext(IConstructionCallMessage msg);

		// Token: 0x060058CC RID: 22732
		[SecurityCritical]
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		// Token: 0x060058CD RID: 22733
		[SecurityCritical]
		void CollectFromServerContext(IConstructionReturnMessage msg);

		// Token: 0x060058CE RID: 22734
		[SecurityCritical]
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);
	}
}
