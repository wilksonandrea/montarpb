using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000898 RID: 2200
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x06005D23 RID: 23843 RVA: 0x0014679F File Offset: 0x0014499F
		internal ConstructionLevelActivator()
		{
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x001467A7 File Offset: 0x001449A7
		// (set) Token: 0x06005D25 RID: 23845 RVA: 0x001467AA File Offset: 0x001449AA
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005D26 RID: 23846 RVA: 0x001467B1 File Offset: 0x001449B1
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x001467B4 File Offset: 0x001449B4
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoServerContextActivation(ctorMsg);
		}
	}
}
