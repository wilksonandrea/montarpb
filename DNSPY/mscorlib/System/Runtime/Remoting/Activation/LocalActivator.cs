using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000894 RID: 2196
	[SecurityCritical]
	internal class LocalActivator : ContextAttribute, IActivator
	{
		// Token: 0x06005D08 RID: 23816 RVA: 0x001462A2 File Offset: 0x001444A2
		internal LocalActivator()
			: base("RemoteActivationService.rem")
		{
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x001462B0 File Offset: 0x001444B0
		[SecurityCritical]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (RemotingConfigHandler.Info == null)
			{
				return true;
			}
			RuntimeType runtimeType = ctorMsg.ActivationType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfigHandler.IsWellKnownClientType(runtimeType);
			string text = ((wellKnownClientTypeEntry == null) ? null : wellKnownClientTypeEntry.ObjectUrl);
			if (text != null)
			{
				ctorMsg.Properties["Connect"] = text;
				return false;
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.IsRemotelyActivatedClientType(runtimeType);
			string text2 = null;
			if (activatedClientTypeEntry == null)
			{
				object[] callSiteActivationAttributes = ctorMsg.CallSiteActivationAttributes;
				if (callSiteActivationAttributes != null)
				{
					for (int i = 0; i < callSiteActivationAttributes.Length; i++)
					{
						UrlAttribute urlAttribute = callSiteActivationAttributes[i] as UrlAttribute;
						if (urlAttribute != null)
						{
							text2 = urlAttribute.UrlValue;
						}
					}
				}
				if (text2 == null)
				{
					return true;
				}
			}
			else
			{
				text2 = activatedClientTypeEntry.ApplicationUrl;
			}
			string text3;
			if (!text2.EndsWith("/", StringComparison.Ordinal))
			{
				text3 = text2 + "/RemoteActivationService.rem";
			}
			else
			{
				text3 = text2 + "RemoteActivationService.rem";
			}
			ctorMsg.Properties["Remote"] = text3;
			return false;
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x001463AC File Offset: 0x001445AC
		[SecurityCritical]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg.Properties.Contains("Remote"))
			{
				string text = (string)ctorMsg.Properties["Remote"];
				AppDomainLevelActivator appDomainLevelActivator = new AppDomainLevelActivator(text);
				IActivator activator = ctorMsg.Activator;
				if (activator.Level < ActivatorLevel.AppDomain)
				{
					appDomainLevelActivator.NextActivator = activator;
					ctorMsg.Activator = appDomainLevelActivator;
					return;
				}
				if (activator.NextActivator == null)
				{
					activator.NextActivator = appDomainLevelActivator;
					return;
				}
				while (activator.NextActivator.Level >= ActivatorLevel.AppDomain)
				{
					activator = activator.NextActivator;
				}
				appDomainLevelActivator.NextActivator = activator.NextActivator;
				activator.NextActivator = appDomainLevelActivator;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005D0B RID: 23819 RVA: 0x00146441 File Offset: 0x00144641
		// (set) Token: 0x06005D0C RID: 23820 RVA: 0x00146444 File Offset: 0x00144644
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

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06005D0D RID: 23821 RVA: 0x0014644B File Offset: 0x0014464B
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x00146450 File Offset: 0x00144650
		private static MethodBase GetMethodBase(IConstructionCallMessage msg)
		{
			MethodBase methodBase = msg.MethodBase;
			if (null == methodBase)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), msg.MethodName, msg.TypeName));
			}
			return methodBase;
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x00146494 File Offset: 0x00144694
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctorMsg.Properties.Contains("Remote"))
			{
				return LocalActivator.DoRemoteActivation(ctorMsg);
			}
			if (ctorMsg.Properties.Contains("Permission"))
			{
				Type activationType = ctorMsg.ActivationType;
				object[] array = null;
				if (activationType.IsContextful)
				{
					IList contextProperties = ctorMsg.ContextProperties;
					if (contextProperties != null && contextProperties.Count > 0)
					{
						RemotePropertyHolderAttribute remotePropertyHolderAttribute = new RemotePropertyHolderAttribute(contextProperties);
						array = new object[] { remotePropertyHolderAttribute };
					}
				}
				MethodBase methodBase = LocalActivator.GetMethodBase(ctorMsg);
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
				object[] array2 = Message.CoerceArgs(ctorMsg, reflectionCachedData.Parameters);
				object obj = Activator.CreateInstance(activationType, array2, array);
				if (RemotingServices.IsClientProxy(obj))
				{
					RedirectionProxy redirectionProxy = new RedirectionProxy((MarshalByRefObject)obj, activationType);
					RemotingServices.MarshalInternal(redirectionProxy, null, activationType);
					obj = redirectionProxy;
				}
				return ActivationServices.SetupConstructionReply(obj, ctorMsg, null);
			}
			return ctorMsg.Activator.Activate(ctorMsg);
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x0014657C File Offset: 0x0014477C
		internal static IConstructionReturnMessage DoRemoteActivation(IConstructionCallMessage ctorMsg)
		{
			IActivator activator = null;
			string text = (string)ctorMsg.Properties["Remote"];
			try
			{
				activator = (IActivator)RemotingServices.Connect(typeof(IActivator), text);
			}
			catch (Exception ex)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_ConnectFailed"), ex));
			}
			ctorMsg.Properties.Remove("Remote");
			return activator.Activate(ctorMsg);
		}
	}
}
