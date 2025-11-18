using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x020001D9 RID: 473
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class HostSecurityManager
	{
		// Token: 0x06001C9F RID: 7327 RVA: 0x00061E64 File Offset: 0x00060064
		public HostSecurityManager()
		{
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00061E6C File Offset: 0x0006006C
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00061E70 File Offset: 0x00060070
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public virtual PolicyLevel DomainPolicy
		{
			get
			{
				if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
				}
				return null;
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00061E8F File Offset: 0x0006008F
		public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
		{
			return inputEvidence;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00061E92 File Offset: 0x00060092
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00061E98 File Offset: 0x00060098
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
		{
			if (applicationEvidence == null)
			{
				throw new ArgumentNullException("applicationEvidence");
			}
			ActivationArguments hostEvidence = applicationEvidence.GetHostEvidence<ActivationArguments>();
			if (hostEvidence == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
			}
			ActivationContext activationContext = hostEvidence.ActivationContext;
			if (activationContext == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
			}
			ApplicationTrust applicationTrust = applicationEvidence.GetHostEvidence<ApplicationTrust>();
			if (applicationTrust != null && !CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
			{
				applicationTrust = null;
			}
			if (applicationTrust == null)
			{
				if (AppDomain.CurrentDomain.ApplicationTrust != null && CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ApplicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
				{
					applicationTrust = AppDomain.CurrentDomain.ApplicationTrust;
				}
				else
				{
					applicationTrust = ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context);
				}
			}
			ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
			if (applicationTrust != null && applicationTrust.IsApplicationTrustedToRun && !applicationSecurityInfo.DefaultRequestSet.IsSubsetOf(applicationTrust.DefaultGrantSet.PermissionSet))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Policy_AppTrustMustGrantAppRequest"));
			}
			return applicationTrust;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00061F84 File Offset: 0x00060184
		public virtual PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (evidence.GetHostEvidence<GacInstalled>() != null)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			if (AppDomain.CurrentDomain.IsHomogenous)
			{
				return AppDomain.CurrentDomain.GetHomogenousGrantSet(evidence);
			}
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			return SecurityManager.PolicyManager.CodeGroupResolve(evidence, false);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00061FE5 File Offset: 0x000601E5
		public virtual Type[] GetHostSuppliedAppDomainEvidenceTypes()
		{
			return null;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00061FE8 File Offset: 0x000601E8
		public virtual Type[] GetHostSuppliedAssemblyEvidenceTypes(Assembly assembly)
		{
			return null;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00061FEB File Offset: 0x000601EB
		public virtual EvidenceBase GenerateAppDomainEvidence(Type evidenceType)
		{
			return null;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00061FEE File Offset: 0x000601EE
		public virtual EvidenceBase GenerateAssemblyEvidence(Type evidenceType, Assembly assembly)
		{
			return null;
		}
	}
}
