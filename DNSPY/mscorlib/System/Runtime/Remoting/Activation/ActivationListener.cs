using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000895 RID: 2197
	internal class ActivationListener : MarshalByRefObject, IActivator
	{
		// Token: 0x06005D11 RID: 23825 RVA: 0x001465FC File Offset: 0x001447FC
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x001465FF File Offset: 0x001447FF
		// (set) Token: 0x06005D13 RID: 23827 RVA: 0x00146602 File Offset: 0x00144802
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

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x00146609 File Offset: 0x00144809
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x00146610 File Offset: 0x00144810
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null || RemotingServices.IsTransparentProxy(ctorMsg))
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.Properties["Permission"] = "allowed";
			string activationTypeName = ctorMsg.ActivationTypeName;
			if (!RemotingConfigHandler.IsActivationAllowed(activationTypeName))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_PermissionDenied"), ctorMsg.ActivationTypeName));
			}
			Type activationType = ctorMsg.ActivationType;
			if (activationType == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), ctorMsg.ActivationTypeName));
			}
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x001466B2 File Offset: 0x001448B2
		public ActivationListener()
		{
		}
	}
}
