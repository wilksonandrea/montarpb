using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Security.Policy
{
	// Token: 0x02000340 RID: 832
	internal sealed class AppDomainEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x0600296C RID: 10604 RVA: 0x00098D6C File Offset: 0x00096F6C
		internal AppDomainEvidenceFactory(AppDomain target)
		{
			this.m_targetDomain = target;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x00098D7B File Offset: 0x00096F7B
		public IEvidenceFactory Target
		{
			get
			{
				return this.m_targetDomain;
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00098D83 File Offset: 0x00096F83
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			return new EvidenceBase[0];
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00098D8C File Offset: 0x00096F8C
		[SecuritySafeCritical]
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			if (!this.m_targetDomain.IsDefaultAppDomain())
			{
				AppDomain defaultDomain = AppDomain.GetDefaultDomain();
				return defaultDomain.GetHostEvidence(evidenceType);
			}
			if (this.m_entryPointEvidence == null)
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				RuntimeAssembly runtimeAssembly = entryAssembly as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					this.m_entryPointEvidence = runtimeAssembly.EvidenceNoDemand.Clone();
				}
				else if (entryAssembly != null)
				{
					this.m_entryPointEvidence = entryAssembly.Evidence;
				}
			}
			if (this.m_entryPointEvidence != null)
			{
				return this.m_entryPointEvidence.GetHostEvidence(evidenceType);
			}
			return null;
		}

		// Token: 0x04001108 RID: 4360
		private AppDomain m_targetDomain;

		// Token: 0x04001109 RID: 4361
		private Evidence m_entryPointEvidence;
	}
}
