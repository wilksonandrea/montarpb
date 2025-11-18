using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000897 RID: 2199
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06005D1D RID: 23837 RVA: 0x0014672D File Offset: 0x0014492D
		internal ContextLevelActivator()
		{
			this.m_NextActivator = null;
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0014673C File Offset: 0x0014493C
		internal ContextLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06005D1F RID: 23839 RVA: 0x00146772 File Offset: 0x00144972
		// (set) Token: 0x06005D20 RID: 23840 RVA: 0x0014677A File Offset: 0x0014497A
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return this.m_NextActivator;
			}
			[SecurityCritical]
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005D21 RID: 23841 RVA: 0x00146783 File Offset: 0x00144983
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00146786 File Offset: 0x00144986
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoCrossContextActivation(ctorMsg);
		}

		// Token: 0x040029F8 RID: 10744
		private IActivator m_NextActivator;
	}
}
