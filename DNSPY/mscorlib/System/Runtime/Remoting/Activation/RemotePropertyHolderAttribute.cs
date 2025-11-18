using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000899 RID: 2201
	internal class RemotePropertyHolderAttribute : IContextAttribute
	{
		// Token: 0x06005D28 RID: 23848 RVA: 0x001467CD File Offset: 0x001449CD
		internal RemotePropertyHolderAttribute(IList cp)
		{
			this._cp = cp;
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x001467DC File Offset: 0x001449DC
		[SecurityCritical]
		[ComVisible(true)]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x001467E0 File Offset: 0x001449E0
		[SecurityCritical]
		[ComVisible(true)]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			for (int i = 0; i < this._cp.Count; i++)
			{
				ctorMsg.ContextProperties.Add(this._cp[i]);
			}
		}

		// Token: 0x040029F9 RID: 10745
		private IList _cp;
	}
}
