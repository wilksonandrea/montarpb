using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	// Token: 0x02000A58 RID: 2648
	[ComVisible(true)]
	public class ApplicationActivator
	{
		// Token: 0x060066CA RID: 26314 RVA: 0x00159CDC File Offset: 0x00157EDC
		public ApplicationActivator()
		{
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x00159CE4 File Offset: 0x00157EE4
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			return this.CreateInstance(activationContext, null);
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x00159CF0 File Offset: 0x00157EF0
		[SecuritySafeCritical]
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			if (CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ActivationContext, activationContext))
			{
				ManifestRunner manifestRunner = new ManifestRunner(AppDomain.CurrentDomain, activationContext);
				return new ObjectHandle(manifestRunner.ExecuteAsAssembly());
			}
			AppDomainSetup appDomainSetup = new AppDomainSetup(new ActivationArguments(activationContext, activationCustomData));
			AppDomainSetup setupInformation = AppDomain.CurrentDomain.SetupInformation;
			appDomainSetup.AppDomainManagerType = setupInformation.AppDomainManagerType;
			appDomainSetup.AppDomainManagerAssembly = setupInformation.AppDomainManagerAssembly;
			return ApplicationActivator.CreateInstanceHelper(appDomainSetup);
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x00159D70 File Offset: 0x00157F70
		[SecuritySafeCritical]
		protected static ObjectHandle CreateInstanceHelper(AppDomainSetup adSetup)
		{
			if (adSetup.ActivationArguments == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MissingActivationArguments"));
			}
			adSetup.ActivationArguments.ActivateInstance = true;
			Evidence evidence = AppDomain.CurrentDomain.Evidence;
			Evidence evidence2 = CmsUtils.MergeApplicationEvidence(null, adSetup.ActivationArguments.ApplicationIdentity, adSetup.ActivationArguments.ActivationContext, adSetup.ActivationArguments.ActivationData);
			HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
			ApplicationTrust applicationTrust = hostSecurityManager.DetermineApplicationTrust(evidence2, evidence, new TrustManagerContext());
			if (applicationTrust == null || !applicationTrust.IsApplicationTrustedToRun)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, null);
			}
			ObjRef objRef = AppDomain.nCreateInstance(adSetup.ActivationArguments.ApplicationIdentity.FullName, adSetup, evidence2, (evidence2 == null) ? AppDomain.CurrentDomain.InternalEvidence : null, AppDomain.CurrentDomain.GetSecurityDescriptor());
			if (objRef == null)
			{
				return null;
			}
			return RemotingServices.Unmarshal(objRef) as ObjectHandle;
		}
	}
}
