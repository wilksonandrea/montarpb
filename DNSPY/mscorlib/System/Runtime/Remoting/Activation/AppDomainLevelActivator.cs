using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000896 RID: 2198
	[Serializable]
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x06005D17 RID: 23831 RVA: 0x001466BA File Offset: 0x001448BA
		internal AppDomainLevelActivator(string remActivatorURL)
		{
			this.m_RemActivatorURL = remActivatorURL;
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x001466C9 File Offset: 0x001448C9
		internal AppDomainLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x001466FF File Offset: 0x001448FF
		// (set) Token: 0x06005D1A RID: 23834 RVA: 0x00146707 File Offset: 0x00144907
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

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06005D1B RID: 23835 RVA: 0x00146710 File Offset: 0x00144910
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x00146714 File Offset: 0x00144914
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = this.m_NextActivator;
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}

		// Token: 0x040029F6 RID: 10742
		private IActivator m_NextActivator;

		// Token: 0x040029F7 RID: 10743
		private string m_RemActivatorURL;
	}
}
