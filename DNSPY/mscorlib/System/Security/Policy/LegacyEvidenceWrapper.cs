using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000350 RID: 848
	[Serializable]
	internal sealed class LegacyEvidenceWrapper : EvidenceBase, ILegacyEvidenceAdapter
	{
		// Token: 0x06002A44 RID: 10820 RVA: 0x0009C94C File Offset: 0x0009AB4C
		internal LegacyEvidenceWrapper(object legacyEvidence)
		{
			this.m_legacyEvidence = legacyEvidence;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x0009C95B File Offset: 0x0009AB5B
		public object EvidenceObject
		{
			get
			{
				return this.m_legacyEvidence;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x0009C963 File Offset: 0x0009AB63
		public Type EvidenceType
		{
			get
			{
				return this.m_legacyEvidence.GetType();
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0009C970 File Offset: 0x0009AB70
		public override bool Equals(object obj)
		{
			return this.m_legacyEvidence.Equals(obj);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0009C97E File Offset: 0x0009AB7E
		public override int GetHashCode()
		{
			return this.m_legacyEvidence.GetHashCode();
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0009C98B File Offset: 0x0009AB8B
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x0400113D RID: 4413
		private object m_legacyEvidence;
	}
}
