using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089D RID: 2205
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005D33 RID: 23859
		// (set) Token: 0x06005D34 RID: 23860
		IActivator Activator
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005D35 RID: 23861
		object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005D36 RID: 23862
		string ActivationTypeName
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06005D37 RID: 23863
		Type ActivationType
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06005D38 RID: 23864
		IList ContextProperties
		{
			[SecurityCritical]
			get;
		}
	}
}
